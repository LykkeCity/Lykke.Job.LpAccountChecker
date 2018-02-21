using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Job.LpAccountChecker.Core.Domain;

namespace Lykke.Job.LpAccountChecker.Core.Services
{
    public interface IExchangePollingService
    {
        void Initialize(Dictionary<string, IReadOnlyList<ExchangeCheckRule>> exchangeRules);
        
        Task Poll(string exchangeName, TimeSpan timeout);
    }
}
