using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.WebApi.PluginSystem.Loaders;

namespace TilesDashboard.PluginBase.Notification
{
    public class NotificationPluginLoader : PluginLoaderBase, INotificationPluginLoader
    {
        private readonly ILogger<NotificationPluginLoader> _logger;

        private readonly string _pluginFolder = "plugins";

        public NotificationPluginLoader(ILogger<NotificationPluginLoader> logger)
            : base(logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<IList<INotificationPlugin>> LoadNotificationPluginsAsync(string rootPath)
        {
            _logger.LogInformation("Loading Notification plugins...");

            var pluginPaths = GetPluginsPaths(rootPath, _pluginFolder);
            var loadedPlugins = LoadDataPluginsFromDlls<INotificationPlugin>(pluginPaths);
            return Task.FromResult(loadedPlugins);
        }
    }
}
