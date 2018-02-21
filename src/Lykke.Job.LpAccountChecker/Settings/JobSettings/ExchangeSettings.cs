namespace Lykke.Job.LpAccountChecker.Settings.JobSettings
{
    public class ExchangeSettings
    {
        public string ExchangeName { get; set; }
        
        public ExchangeConnectorSettings ExchangeConnector { get; set; }
        
        public ExchangeCheckRuleSettings[] ExchangeCheckRules { get; set; } 
    }
}
