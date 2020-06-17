using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class PluginLoader : IPluginLoader
    {
        private readonly ILogger<PluginLoader> _logger;

        private readonly string _pluginFolder = "plugins";

        private readonly IPluginConfigProvider _pluginConfigProvider;

        public PluginLoader(ILogger<PluginLoader> logger, IPluginConfigProvider pluginConfigProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pluginConfigProvider = pluginConfigProvider ?? throw new ArgumentNullException(nameof(pluginConfigProvider));
        }

        public async Task<Plugins> LoadPluginsAsync(string rootPath)
        {
            _logger.LogInformation("Loading plugins...");
            var pluginsFolder = Path.Combine(rootPath, _pluginFolder);
            if (!Directory.Exists(pluginsFolder))
            {
                _logger.LogInformation("Plugin folder does not exist, plugins will not be loaded.");
                return Plugins.NoPluginsLoaded;
            }

            var pluginPaths = Directory.GetFiles(pluginsFolder);
            if (pluginPaths.IsEmpty())
            {
                _logger.LogInformation("There is no plugins to load.");
                return Plugins.NoPluginsLoaded;
            }

            var loadedPlugins = LoadPluginsFromDlls(pluginPaths);
            var initializedPlugins = await InitializePlugins(loadedPlugins);

            return initializedPlugins;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        private async Task<Plugins> InitializePlugins(Plugins loadedPlugins)
        {
            _logger.LogInformation("Initialazing plugins...");

            var initializedPlugins = new Plugins();
            foreach (var plugin in loadedPlugins.WeatherPlugins)
            {
                try
                {
                    await plugin.InitializeAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Weather plugin: \"{plugin.TileName}\" threw exception during initialization. Plugin will be disabled. Error: {ex.Message}", ex);
                    break;
                }

                initializedPlugins.WeatherPlugins.Add(plugin);
            }

            _logger.LogInformation("Plugins have been initialized.");
            return initializedPlugins;
        }

        private Plugins LoadPluginsFromDlls(string[] pluginPaths)
        {
            var loadedPlugins = new Plugins();
            foreach (var pluginPath in pluginPaths)
            {
                _logger.LogInformation($"Loading plugins from {pluginPath}.");
                PluginLoadContext loadContext = new PluginLoadContext(pluginPath);
                var pluginAssembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
                loadedPlugins.Merge(LoadPluginsFromAssembly(pluginAssembly));
            }

            _logger.LogInformation("Loading plugins completed.");
            return loadedPlugins;
        }

        private Plugins LoadPluginsFromAssembly(Assembly assembly)
        {
            var weatherPlugins = new List<BaseWeatherPlugin>();
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(BaseWeatherPlugin).IsAssignableFrom(type))
                {
                    BaseWeatherPlugin plugin;
                    if (type.GetConstructor(new[] { typeof(IPluginConfigProvider) }).Exists())
                    {
                        plugin = Activator.CreateInstance(type, _pluginConfigProvider) as BaseWeatherPlugin;
                    }
                    else
                    {
                        plugin = Activator.CreateInstance(type) as BaseWeatherPlugin;
                    }

                    weatherPlugins.Add(plugin);
                }
            }

            _logger.LogInformation($"Loaded: {weatherPlugins.Count} Weather plugins");
            return new Plugins(weatherPlugins);
        }
    }
}
