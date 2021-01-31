using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TilesDashboard.Contract.Events;
using TilesDashboard.Handy.Events;

namespace TilesDashboard.WebApi.Hubs
{
    public class TilesNotificationHub : Hub, IEventHandler<NewDataEvent>
    {
        private readonly IHubContext<TilesNotificationHub> _context;

        public TilesNotificationHub(IHubContext<TilesNotificationHub> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task ExecuteAsync(NewDataEvent eventBody, CancellationToken cancellationToken)
        {
            await _context.Clients.All.SendAsync("NewData", eventBody.TileId.Name, eventBody.TileId.Type.ToString(), eventBody.TileValue, cancellationToken);
        }
    }
}
