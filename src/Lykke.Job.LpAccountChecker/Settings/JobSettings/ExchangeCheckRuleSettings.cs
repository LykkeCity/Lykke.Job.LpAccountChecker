namespace Lykke.Job.LpAccountChecker.Settings.JobSettings
{
    public class ExchangeCheckRuleSettings
    {
        public string SlackNotificationLevel { get; set; }
        
        public int MaxPercentage { get; set; }
        
        public string ExchangeCheckType {get; set; }
        
        public string Message { get; set; }
    }
}
