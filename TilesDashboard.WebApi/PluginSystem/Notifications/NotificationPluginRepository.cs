using System;
using System.Collections.Generic;
using System.Linq;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public class NotificationPluginRepository : INotificationPluginRepository
    {
        private readonly INotificationPluginContext _notificationPluginContext;

        public NotificationPluginRepository(INotificationPluginContext notificationPluginContext)
        {
            _notificationPluginContext = notificationPluginContext ?? throw new ArgumentNullException(nameof(notificationPluginContext));
        }

        public IList<INotificationPlugin> FindNotificationPluginsByTileType(TileType tileType)
        {
            return _notificationPluginContext.NotificationPlugins.Where(x => x.TileType == tileType).ToList();
        }
    }
}
