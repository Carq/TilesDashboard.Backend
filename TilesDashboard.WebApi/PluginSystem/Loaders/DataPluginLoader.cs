using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.WebApi.PluginSystem.Loaders;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class DataPluginLoader : PluginLoaderBase, IDataPluginLoader
    {
        private readonly ILogger<DataPluginLoader> _logger;

        private readonly string _pluginFolder = "plugins";

        public DataPluginLoader(ILogger<DataPluginLoader> logger, IPluginConfigProvider pluginConfigProvider)
            : base(pluginConfigProvider, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<IDataPlugin>> LoadDataProviderPluginsAsync(string rootPath)
        {
            _logger.LogInformation("Loading Data plugins...");
            var pluginPaths = GetPluginsPaths(rootPath, _pluginFolder);
            var loadedPlugins = LoadDataPluginsFromDlls<IDataPlugin>(pluginPaths);
            var initializedPlugins = await InitializePlugins(loadedPlugins);

            return initializedPlugins;
        }
    }
}
