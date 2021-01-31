﻿using System;
using System.Collections.Generic;
using System.Linq;
using TilesDashboard.PluginBase.Notification;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public class NotificationPluginRepository : INotificationPluginRepository
    {
        private readonly INotificationPluginContext _notificationPluginContext;

        public NotificationPluginRepository(INotificationPluginContext notificationPluginContext)
        {
            _notificationPluginContext = notificationPluginContext ?? throw new ArgumentNullException(nameof(notificationPluginContext));
        }

        public IList<INotificationPlugin> FindNotificationPlugins(IList<string> pluginNames)
        {
            return _notificationPluginContext.NotificationPlugins.Where(x => pluginNames.Contains(x.UniquePluginName)).ToList();
        }
    }
}
