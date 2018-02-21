using System.Threading.Tasks;
using Autofac;
using Common;
using Lykke.Job.LpAccountChecker.Contract;

namespace Lykke.Job.LpAccountChecker.Core.Services
{
    public interface IMyRabbitPublisher : IStartable, IStopable
    {
        Task PublishAsync(MyPublishedMessage message);
    }
}