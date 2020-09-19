using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.Notification;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public class NotificationPluginContext : INotificationPluginContext
    {
        public NotificationPluginContext()
        {
        }

        public IList<INotificationPlugin> NotificationPlugins { get; } = new List<INotificationPlugin>();

        public void AddPlugins(IList<INotificationPlugin> notificationPlugins)
        {
            NotificationPlugins.AddRange(notificationPlugins);
        }
    }
}
