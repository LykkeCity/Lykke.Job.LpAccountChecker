namespace Lykke.Job.LpAccountChecker.Core.Domain
{
    public class ExchangeCheckRule
    {
        public SlackNotificationLevel SlackNotificationLevel { get; set; }
        
        public int MaxPercentage { get; set; }
        
        public ExchangeCheckType ExchangeCheckType {get; set; }
        
        public string Message { get; set; }
    }
}
