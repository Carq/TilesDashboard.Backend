using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.WebApi.Configuration;

namespace TilesDashboard.WebApi.PluginInfrastructure
{
    public class PluginLoader : IPluginLoader
    {
        private readonly ILogger<PluginLoader> _logger;

        private readonly string _pluginFolder = "plugins";

        public PluginLoader(ILogger<PluginLoader> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public LoadedPlugins LoadPlugins(string rootPath)
        {
            _logger.LogInformation("Initializing plugins...");
            var pluginsFolder = Path.Combine(rootPath, _pluginFolder);
            if (!Directory.Exists(pluginsFolder))
            {
                _logger.LogInformation("Plugin folder does not exist, plugins will not be loaded.");
                return LoadedPlugins.NoPluginsLoaded;
            }

            var pluginPaths = Directory.GetFiles(pluginsFolder);
            if (pluginPaths.IsEmpty())
            {
                _logger.LogInformation("There is no plugins to load.");
                return LoadedPlugins.NoPluginsLoaded;
            }

            var loadedPlugins = new LoadedPlugins();
            foreach (var pluginPath in pluginPaths)
            {
                _logger.LogInformation($"Loading plugins from {pluginPath}.");
                PluginLoadContext loadContext = new PluginLoadContext(pluginPath);
                var pluginAssembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
                loadedPlugins.Merge(LoadPluginsFromAssembly(pluginAssembly));
            }

            _logger.LogInformation("Loading plugins compelted.");
            return loadedPlugins;
        }

        private LoadedPlugins LoadPluginsFromAssembly(Assembly assembly)
        {
            var weatherPlugins = new List<IWeatherPlugin>();
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IWeatherPlugin).IsAssignableFrom(type))
                {
                    IWeatherPlugin result = Activator.CreateInstance(type) as IWeatherPlugin;
                    weatherPlugins.Add(result);
                }
            }

            _logger.LogInformation($"Loaded: {weatherPlugins.Count} Weather plugins");
            return new LoadedPlugins(weatherPlugins);
        }
    }
}
