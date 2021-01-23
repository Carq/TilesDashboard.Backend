using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.Data;

namespace TilesDashboard.WebApi.PluginSystem.Loaders
{
    public class PluginLoaderBase
    {
        private readonly ILogger _logger;

        protected PluginLoaderBase(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected string[] GetPluginsPaths(string rootPath, string pluginFolder)
        {
            var pluginsFolder = Path.Combine(rootPath, pluginFolder);
            if (!Directory.Exists(pluginsFolder))
            {
                _logger.LogInformation("Plugin folder does not exist, plugins are not be loaded.");
                return Array.Empty<string>();
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
           where TPluginType : class, IDataPlugin
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

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        protected IList<TPluginType> LoadDataPluginsFromAssembly<TPluginType>(Assembly assembly)
            where TPluginType : class, IDataPlugin
        {
            var plugins = new List<TPluginType>();
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(TPluginType).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    try
                    {
                        TPluginType plugin = Activator.CreateInstance(type) as TPluginType;
                        plugins.Add(plugin);
                        _logger.LogInformation($"Loaded plugin {plugin.UniquePluginName} for tiles type {plugin.TileType}.");
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError($"Plugin initializiaton failed for type {type.FullName} from assembly {assembly.FullName}, exception message: {exception.Message}");
                    }
                }
            }

            return plugins;
        }
    }
}
