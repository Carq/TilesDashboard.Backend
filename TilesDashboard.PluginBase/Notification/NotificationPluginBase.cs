using System.Threading.Tasks;
using TilesDashboard.Core.Type;

namespace TilesDashboard.PluginBase.Notification
{
    public abstract class NotificationPluginBase : INotificationPlugin
    {
        /// <inheritdoc/>
        public abstract TileId TileId { get; }

        public Task InitializeAsync()
        {
            throw new System.NotImplementedException();
        }

        public abstract Task PerformNotification();
    }
}
