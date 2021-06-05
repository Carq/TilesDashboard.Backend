using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.PluginSystem.Repositories;
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
            InitializePluginsStorage(notificationPlugins, applicationBuilder.ApplicationServices.GetRequiredService<IPluginConfigRepository>()).Wait();

            notificationPluginContext.AddPlugins(notificationPlugins);
        }

        private static async Task InitializePluginsStorage(IList<INotificationPlugin> loadedPlugins, IPluginConfigRepository pluginConfigRepository)
        {
            foreach (var plugin in loadedPlugins)
            {
                if (!await pluginConfigRepository.IsAnyPluginConfigurationExist(plugin.UniquePluginName, CancellationToken.None))
                {
                    await pluginConfigRepository.CreatePluginConfigurationWithTemplateEntry(plugin.UniquePluginName, plugin.TileType, plugin.PluginType, CancellationToken.None);
                }
            }
        }
    }
}
