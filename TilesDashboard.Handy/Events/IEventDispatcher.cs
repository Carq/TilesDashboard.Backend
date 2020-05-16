using System.Threading;
using System.Threading.Tasks;

namespace TilesDashboard.Handy.Events
{
    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent eventBody, CancellationToken cancellationToken)
            where TEvent : IEvent;
    }
}
