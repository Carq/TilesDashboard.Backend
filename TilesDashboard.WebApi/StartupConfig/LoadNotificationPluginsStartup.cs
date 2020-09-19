using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TilesDashboard.WebApi.PluginSystem.Loaders;
using TilesDashboard.WebApi.PluginSystem.Notifications;

namespace TilesDashboard.WebApi.StartupConfig
{
    public static class LoadNotificationPluginsStartup
    {
        public static void LoadNotificationPlugins(this IApplicationBuilder applicationBuilder)
        {
            var notificationPluginContext = applicationBuilder.ApplicationServices.GetService<INotificationPluginContext>();
            var notificationPluginLoader = applicationBuilder.ApplicationServices.GetService<INotificationPluginLoader>();

            var notificationPlugins = notificationPluginLoader.LoadNotificationPluginsAsync(AppDomain.CurrentDomain.BaseDirectory).Result;
            notificationPluginContext.AddPlugins(notificationPlugins);
        }
    }
}
