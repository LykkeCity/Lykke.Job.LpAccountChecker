using System.Threading.Tasks;

namespace Lykke.Job.LpAccountChecker.Core.Services
{
    public interface IStartupManager
    {
        Task StartAsync();
    }
}