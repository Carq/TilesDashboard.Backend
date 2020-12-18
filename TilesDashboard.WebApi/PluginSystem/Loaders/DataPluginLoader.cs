using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.PluginBase.V2;
using TilesDashboard.WebApi.PluginSystem.Loaders;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class DataPluginLoader : PluginLoaderBase, IDataPluginLoader
    {
        private readonly ILogger<DataPluginLoader> _logger;

        private readonly string _pluginFolder = "plugins";

        public DataPluginLoader(ILogger<DataPluginLoader> logger)
            : base(logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<IList<IDataPlugin>> LoadDataPluginsAsync(string rootPath)
        {
            _logger.LogInformation("Loading Data plugins...");
            var pluginPaths = GetPluginsPaths(rootPath, _pluginFolder);
            return Task.FromResult(LoadDataPluginsFromDlls<IDataPlugin>(pluginPaths));
        }
    }
}
