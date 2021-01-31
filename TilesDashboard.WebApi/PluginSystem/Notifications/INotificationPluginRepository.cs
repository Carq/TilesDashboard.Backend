using System.Collections.Generic;
using TilesDashboard.PluginBase.Notification;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public interface INotificationPluginRepository
    {
        IList<INotificationPlugin> FindNotificationPlugins(IList<string> pluginNames);
    }
}
