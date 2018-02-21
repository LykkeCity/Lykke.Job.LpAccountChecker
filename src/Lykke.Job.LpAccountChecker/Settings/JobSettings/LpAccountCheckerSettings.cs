namespace Lykke.Job.LpAccountChecker.Settings.JobSettings
{
    public class LpAccountCheckerSettings
    {
        public DbSettings Db { get; set; }
        
        public ExchangeSettings[] ExchangesSettings { get; set; }
        
        public int AccountControlPollingPeriodMilliseconds { get; set; }
    }
}
