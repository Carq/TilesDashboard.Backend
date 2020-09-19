using System.Threading.Tasks;

namespace TilesDashboard.PluginBase.Notification
{
    public interface INotificationPlugin : IBasePlugin
    {
        Task PerformNotification();

        PluginType IBasePlugin.Type => PluginType.Notification;
    }
}
