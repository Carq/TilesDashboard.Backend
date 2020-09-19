using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;

namespace TilesDashboard.WebApi.PluginSystem.Loaders
{
    public class PluginLoaderBase
    {
        private readonly ILogger _logger;

        private readonly IPluginConfigProvider _pluginConfigProvider;

        protected PluginLoaderBase(IPluginConfigProvider pluginConfigProvider,  ILogger logger)
        {
            _pluginConfigProvider = pluginConfigProvider ?? throw new ArgumentNullException(nameof(pluginConfigProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        protected async Task<IList<TPluginType>> InitializePlugins<TPluginType>(IList<TPluginType> loadedPlugins)
               where TPluginType : class, IBasePlugin
        {
            _logger.LogInformation("Initialazing plugins...");

            var initializedPlugins = new List<TPluginType>();
            foreach (var plugin in loadedPlugins)
            {
                try
                {
                    await plugin.InitializeAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Data Plugin: {plugin.GetType()} \"{plugin.TileId.TileType}\" threw exception during initialization. Plugin will be disabled. Error: {ex.Message}", ex);
                    continue;
                }

                initializedPlugins.Add(plugin);
                _logger.LogInformation($"Data Plugin \"{plugin.TileId.TileType}\" with name \"{plugin.TileId.TileName} \" have been initialized.");
            }

            _logger.LogInformation("Data Plugins initialization completed.");
            return initializedPlugins;
        }

        protected string[] GetPluginsPaths(string rootPath, string pluginFolder)
        {
            var pluginsFolder = Path.Combine(rootPath, pluginFolder);
            if (!Directory.Exists(pluginsFolder))
            {
                _logger.LogInformation("Plugin folder does not exist, plugins are not be loaded.");
                return null;
            }

            var pluginPaths = Directory.GetFiles(pluginsFolder);
            if (pluginPaths.IsEmpty())
            {
                _logger.LogInformation("There is no plugins to load.");
                return Array.Empty<string>();
            }

            return pluginPaths;
        }

        protected IList<TPluginType> LoadDataPluginsFromDlls<TPluginType>(string[] pluginPaths)
           where TPluginType : class, IBasePlugin
        {
            var loadedPlugins = new List<TPluginType>();
            foreach (var pluginPath in pluginPaths)
            {
                _logger.LogInformation($"Loading Data plugins from {pluginPath}.");
                PluginLoadContext loadContext = new PluginLoadContext(pluginPath);
                var pluginAssembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
                loadedPlugins.AddRange(LoadDataPluginsFromAssembly<TPluginType>(pluginAssembly));
            }

            _logger.LogInformation("Loading plugins completed.");
            return loadedPlugins;
        }

        protected IList<TPluginType> LoadDataPluginsFromAssembly<TPluginType>(Assembly assembly)
            where TPluginType : class, IBasePlugin
        {
            var plugins = new List<TPluginType>();
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(TPluginType).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    TPluginType plugin;
                    if (type.GetConstructor(new[] { typeof(IPluginConfigProvider) }).Exists())
                    {
                        plugin = Activator.CreateInstance(type, _pluginConfigProvider) as TPluginType;
                    }
                    else
                    {
                        plugin = Activator.CreateInstance(type) as TPluginType;
                    }

                    plugins.Add(plugin);
                }
            }

            _logger.LogInformation($"Loaded {plugins.Count} plugins.");
            return plugins;
        }
    }
}
