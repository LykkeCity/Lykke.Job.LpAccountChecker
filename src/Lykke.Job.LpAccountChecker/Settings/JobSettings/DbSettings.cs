using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LpAccountChecker.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
