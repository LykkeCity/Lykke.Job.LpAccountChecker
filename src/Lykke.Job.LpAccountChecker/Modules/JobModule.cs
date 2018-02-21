using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Job.LpAccountChecker.Core.Services;
using Lykke.Job.LpAccountChecker.PeriodicalHandlers;
using Lykke.Job.LpAccountChecker.Services;
using Lykke.Job.LpAccountChecker.Settings.JobSettings;
using Lykke.Service.ExchangeConnector.Client;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;
#if azurequeuesub
using Lykke.JobTriggers.Extenstions;
#endif
#if timeperiod
using Lykke.Job.LykkeJob.PeriodicalHandlers;
#endif
#if rabbitsub
using Lykke.Job.LykkeJob.RabbitSubscribers;
#endif
#if rabbitpub
using Lykke.Job.LykkeJob.Contract;
using Lykke.RabbitMq.Azure;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.Job.LykkeJob.RabbitPublishers;
using AzureStorage.Blob;
#endif

namespace Lykke.Job.LpAccountChecker.Modules
{
    public class JobModule : Module
    {
        private readonly LpAccountCheckerSettings _settings;
        private readonly IReloadingManager<DbSettings> _dbSettingsManager;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public JobModule(LpAccountCheckerSettings settings, IReloadingManager<DbSettings> dbSettingsManager, ILog log)
        {
            _settings = settings;
            _log = log;
            _dbSettingsManager = dbSettingsManager;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            // NOTE: Do not register entire settings in container, pass necessary settings to services which requires them
            // ex:
            // builder.RegisterType<QuotesPublisher>()
            //  .As<IQuotesPublisher>()
            //  .WithParameter(TypedParameter.From(_settings.Rabbit.ConnectionString))

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();
            
            RegisterPeriodicalHandlers(builder);


            // TODO: Add your dependencies here

            builder.RegisterType<ExchangePollingService>()
                .As<IExchangePollingService>()
                .SingleInstance();

            foreach (var exchange in _settings.ExchangesSettings)
            {
                builder.RegisterInstance(new ExchangeConnectorService(new ExchangeConnectorServiceSettings
                    {
                        ServiceUrl = exchange.ExchangeConnector.Url,
                        ApiKey = exchange.ExchangeConnector.ApiKey
                    }))
                    .As<IExchangeConnectorService>();
            }

            builder.Populate(_services);
        }

        private void RegisterPeriodicalHandlers(ContainerBuilder builder)
        {
            // TODO: You should register each periodical handler in DI container as IStartable singleton and autoactivate it

            
            builder.RegisterType<ExchangePollingHandler>()
                .WithParameter(TypedParameter.From(_settings.AccountControlPollingPeriodMilliseconds))
                .WithParameter(TypedParameter.From(_settings.ExchangesSettings))
                .SingleInstance();
        }

    }
}
