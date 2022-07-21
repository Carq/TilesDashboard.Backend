using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.DualPlugin;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;
using TilesDashboard.PluginBase.Data.IntegerPlugin;
using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.PluginBase.Data.WeatherPlugin;
using TilesDashboard.PluginSystem.DataPlugins;
using TilesDashboard.PluginSystem.DataPlugins.Handlers;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.PluginSystem.Repositories;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Exceptions;

namespace TilesDashboard.PluginSystem.Services
{
    public class DataPluginService : IDataPluginService
    {
        private readonly IPluginConfigRepository _pluginConfigRepository;

        private readonly IDataPluginContext _dataPluginContext;

        private readonly MetricPluginHandler _metricPluginHandler;

        private readonly WeatherPluginHandler _weatherPluginHandler;

        private readonly IntegerPluginHandler _integerPluginHandler;

        private readonly HeartBeatPluginHandler _heartBeatPluginHandler;

        private readonly DualPluginHandler _dualPluginHandler;

        public DataPluginService(IPluginConfigRepository pluginConfigRepository, IDataPluginContext dataPluginContext, MetricPluginHandler metricPluginHandler, WeatherPluginHandler weatherPluginHandler, IntegerPluginHandler integerPluginHandler, HeartBeatPluginHandler heartBeatPluginHandler, DualPluginHandler dualPluginHandler)
        {
            _pluginConfigRepository = pluginConfigRepository;
            _dataPluginContext = dataPluginContext;
            _metricPluginHandler = metricPluginHandler;
            _weatherPluginHandler = weatherPluginHandler;
            _integerPluginHandler = integerPluginHandler;
            _heartBeatPluginHandler = heartBeatPluginHandler;
            _dualPluginHandler = dualPluginHandler;
        }

        public async Task ExecuteDataPluginForTile(string pluginName, string tileStorageId, CancellationToken cancellationToken)
        {
            var plugin = _dataPluginContext.DataPlugins.SingleOrDefault(x => x.UniquePluginName == pluginName);
            if (plugin == null)
            {
                throw new NotFoundException($"Plugin with name ${pluginName} does not found.");
            }

            var pluginsConfiguration = await _pluginConfigRepository.GetEnabledDataPluginsConfiguration(cancellationToken);
            var pluginConfig = pluginsConfiguration.SingleOrDefault(x => x.PluginName == pluginName);
            if (pluginConfig == null)
            {
                throw new NotFoundException($"Plugin configuration for plugin ${pluginName} does not found.");
            }

            var pluginTileConfig = pluginConfig.PluginTileConfigs.SingleOrDefault(x => x.TileStorageId == tileStorageId);
            if (pluginTileConfig == null)
            {
                throw new NotFoundException($"There is no plugin configuration for Tile with Id ${tileStorageId}.");
            }

            await HandlePlugin(plugin, pluginTileConfig, cancellationToken);
        }

        public async Task<PluginDataResult> HandlePlugin(IDataPlugin plugin, PluginTileConfig pluginConfigurationForTile, CancellationToken cancellationToken)
        {
            PluginDataResult result;

            switch (plugin.TileType)
            {
                case TileType.Weather:
                    var weatherPlugin = (WeatherPluginBase)plugin;
                    result = await _weatherPluginHandler.HandlePlugin(weatherPlugin, pluginConfigurationForTile, cancellationToken);
                    break;
                case TileType.Metric:
                    var metricPlugin = (MetricDataPlugin)plugin;
                    result = await _metricPluginHandler.HandlePlugin(metricPlugin, pluginConfigurationForTile, cancellationToken);
                    break;
                case TileType.Integer:
                    var integerPlugin = (IntegerDataPlugin)plugin;
                    result = await _integerPluginHandler.HandlePlugin(integerPlugin, pluginConfigurationForTile, cancellationToken);
                    break;
                case TileType.HeartBeat:
                    var heartBeatPlugin = (HeartBeatDataPlugin)plugin;
                    result = await _heartBeatPluginHandler.HandlePlugin(heartBeatPlugin, pluginConfigurationForTile, cancellationToken);
                    break;
                case TileType.Dual:
                    var dualPlugin = (DualPluginBase) plugin;
                    result = await _dualPluginHandler.HandlePlugin(dualPlugin, pluginConfigurationForTile,
                        cancellationToken);
                    break;
                default:
                    throw new NotSupportedException($"Plugin type {plugin.TileType} is not yet supported");
            }

            return result;
        }
    }
}
