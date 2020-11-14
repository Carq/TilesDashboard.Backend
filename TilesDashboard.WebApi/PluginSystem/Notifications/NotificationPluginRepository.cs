using System;
using System.Collections.Generic;
using System.Linq;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public class NotificationPluginRepository : INotificationPluginRepository
    {
        private readonly INotificationPluginContext _notificationPluginContext;

        public NotificationPluginRepository(INotificationPluginContext notificationPluginContext)
        {
            _notificationPluginContext = notificationPluginContext ?? throw new ArgumentNullException(nameof(notificationPluginContext));
        }

        public IList<INotificationPlugin> FindNotificationPlugins(TileId tileId)
        {
            return _notificationPluginContext.NotificationPlugins.Where(x => x.TileId == new TilesDashboard.V2.Core.Entities.TileId(tileId.Name, tileId.Type)).ToList();
        }
    }
}
