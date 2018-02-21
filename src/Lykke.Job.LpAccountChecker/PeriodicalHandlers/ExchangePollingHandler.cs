using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Job.LpAccountChecker.Core.Domain;
using Lykke.Job.LpAccountChecker.Core.Services;
using Lykke.Job.LpAccountChecker.Settings.JobSettings;
using Lykke.Service.ExchangeConnector.Client;
using Lykke.Service.ExchangeConnector.Client.Models;

namespace Lykke.Job.LpAccountChecker.PeriodicalHandlers
{
    public class ExchangePollingHandler : TimerPeriod
    {
        protected readonly TimeSpan PollingPeriod;

        private readonly IExchangePollingService _exchangePollingService;

        private readonly ExchangeSettings[] _exchangesSettings;

        protected ExchangePollingHandler(
            string contextName,
            int pollingPeriodMilliseconds,
            IExchangePollingService exchangePollingService,
            ExchangeSettings[] exchangesSettings,
            ILog log)
            : base(contextName, pollingPeriodMilliseconds, log)
        {
            PollingPeriod = TimeSpan.FromMilliseconds(pollingPeriodMilliseconds);

            _exchangePollingService = exchangePollingService;

            _exchangesSettings = exchangesSettings;
        }

        public void InitializeAndStart()
        {
            var checkRules = _exchangesSettings.ToDictionary(x => x.ExchangeName,
                exchange => (IReadOnlyList<ExchangeCheckRule>) exchange.ExchangeCheckRules.Select(x =>
                    new ExchangeCheckRule
                    {
                        ExchangeCheckType = Enum.Parse<ExchangeCheckType>(x.ExchangeCheckType),
                        MaxPercentage = x.MaxPercentage,
                        Message = x.Message,
                        SlackNotificationLevel = Enum.Parse<SlackNotificationLevel>(x.SlackNotificationLevel)
                    }).ToList());
            
            _exchangePollingService.Initialize(checkRules);
            
            this.Start();
        }

        public override async Task Execute()
        {
            await Task.WhenAll(_exchangesSettings.Select(exchange =>
                _exchangePollingService.Poll(exchange.ExchangeName, PollingPeriod)));
        }
    }
}
