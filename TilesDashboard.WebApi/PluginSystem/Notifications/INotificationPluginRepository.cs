using System.Collections.Generic;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public interface INotificationPluginRepository
    {
        IList<INotificationPlugin> FindNotificationPlugins(TileId tileId);
    }
}
