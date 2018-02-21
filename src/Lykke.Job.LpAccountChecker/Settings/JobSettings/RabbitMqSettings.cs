using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LpAccountChecker.Settings.JobSettings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
#if (rabbitsub)

        public string ExchangeName { get; set; }
#endif
    }
}
