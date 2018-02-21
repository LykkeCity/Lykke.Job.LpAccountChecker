using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Common.Log;
using Lykke.Job.LpAccountChecker.Core.Domain;
using Lykke.Job.LpAccountChecker.Core.Services;
using Lykke.Service.ExchangeConnector.Client;

namespace Lykke.Job.LpAccountChecker.Services
{
    public class ExchangePollingService : IExchangePollingService
    {
        private readonly Dictionary<string, IExchangeConnectorService> _exchangeConnectorServices;

        private Dictionary<string, IReadOnlyList<ExchangeCheckRule>> _exchangeRules;

        private readonly ILog _log;

        public ExchangePollingService(
            IEnumerable<IExchangeConnectorService> _exchangeConnectors,

            ILog log)
        {
            _exchangeConnectorServices = _exchangeConnectors.ToDictionary(x => x.BaseUri.AbsoluteUri, x => x);

            _log = log;
        }

        public void Initialize(Dictionary<string, IReadOnlyList<ExchangeCheckRule>> exchangeRules)
        {
            _exchangeRules = exchangeRules;
        }

        public async Task Poll(string exchangeName, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
    }
}
