using System.Collections.Generic;
using TilesDashboard.Core.Type;
using TilesDashboard.PluginBase.Notification;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public interface INotificationPluginRepository
    {
        IList<INotificationPlugin> FindNotificationPlugins(TileId tileId);
    }
}
