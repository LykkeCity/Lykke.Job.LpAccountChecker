using Lykke.Job.LpAccountChecker.Settings.JobSettings;
using Lykke.Job.LpAccountChecker.Settings.SlackNotifications;

namespace Lykke.Job.LpAccountChecker.Settings
{
    public class AppSettings
    {
        public LpAccountCheckerSettings LpAccountChecker { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
