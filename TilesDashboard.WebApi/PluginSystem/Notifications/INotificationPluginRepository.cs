using System.Collections.Generic;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public interface INotificationPluginRepository
    {
        IList<INotificationPlugin> FindNotificationPluginsByTileType(TileType tileType);
    }
}
