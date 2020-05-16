using System.Threading;
using System.Threading.Tasks;

namespace TilesDashboard.Handy.Events
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task ExecuteAsync(TEvent eventBody, CancellationToken cancellationToken);
    }
}
