using System.Threading;
using System.Threading.Tasks;

namespace TilesDashboard.PluginBase.Notification
{
    public interface INotificationPlugin : IBasePlugin
    {
        Task PerformNotificationAsync(object newData, CancellationToken cancellationToken);

        PluginType IBasePlugin.Type => PluginType.Notification;
    }
}
