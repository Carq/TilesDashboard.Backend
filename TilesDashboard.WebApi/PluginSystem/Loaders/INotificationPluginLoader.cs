using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Notification;

namespace TilesDashboard.WebApi.PluginSystem.Loaders
{
    public interface INotificationPluginLoader
    {
        Task<IList<INotificationPlugin>> LoadNotificationPluginsAsync(string rootPath);
    }
}
