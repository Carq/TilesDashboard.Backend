using System.Collections.Generic;
using TilesDashboard.PluginBase.Notification;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public interface INotificationPluginContext
    {
        IList<INotificationPlugin> NotificationPlugins { get; }

        void AddPlugins(IList<INotificationPlugin> notificationPlugins);
    }
}