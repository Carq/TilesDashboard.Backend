using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Contract.Events;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.PluginSystem.Repositories;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Dual;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.HeartBeat;
using TilesDashboard.V2.Core.Entities.Integer;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Entities.Weather;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public class PluginNotificationHandler : IEventHandler<NewDataEvent>
    {
        private readonly ILogger<PluginNotificationHandler> _logger;

        private readonly INotificationPluginRepository _notificationPluginRepository;

        private readonly IPluginConfigRepository _pluginConfigRepository;

        private readonly ITileRepository _tileRepository;

        public PluginNotificationHandler(INotificationPluginRepository notificationPluginRepository, IPluginConfigRepository pluginConfigRepository, ITileRepository tileRepository, ILogger<PluginNotificationHandler> logger)
        {
            _notificationPluginRepository = notificationPluginRepository ?? throw new ArgumentNullException(nameof(notificationPluginRepository));
            _pluginConfigRepository = pluginConfigRepository ?? throw new ArgumentNullException(nameof(pluginConfigRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tileRepository = tileRepository ?? throw new ArgumentNullException(nameof(tileRepository));
        }

        public async Task ExecuteAsync(NewDataEvent eventBody, CancellationToken cancellationToken)
        {
            var notificationPlugins = _notificationPluginRepository.FindNotificationPluginsByTileType(eventBody.TileId.Type);
            if (notificationPlugins.IsEmpty())
            {
                return;
            }

            var notificationPluginsConfigurations = await _pluginConfigRepository.GetNotificationConfigs(eventBody.TileId.Type, cancellationToken);
            if (notificationPluginsConfigurations.IsEmpty())
            {
                return;
            }

            Dictionary<string, string> tileConfiguration = await GetTileConfiguration(eventBody);
            foreach (var plugin in notificationPlugins)
            {
                var notificationConfigurationsForPlugin = GetPluginConfigs(plugin, eventBody.TileStorageId, notificationPluginsConfigurations);
                _logger.LogInformation($"Plugin {plugin.GetType()} has {notificationConfigurationsForPlugin.Count} notification configurations for {eventBody.TileId}.");
                foreach (var pluginConfigurationForTile in notificationConfigurationsForPlugin.Where(y => y.TileStorageId == eventBody.TileStorageId.Value && y.Disabled == false))
                {
                    await SendNotificationAsync(eventBody, plugin, pluginConfigurationForTile, tileConfiguration, cancellationToken);
                }
            }
        }

        private static IList<NotificationPluginTileConfig> GetPluginConfigs(INotificationPlugin plugin, StorageId tileStorageId, IList<PluginConfiguration> notificationPluginsConfigurations)
        {
            return notificationPluginsConfigurations
                .SingleOrDefault(x => x.PluginName == plugin.UniquePluginName)?
                .NotificationPluginTileConfigs.Where(x => x.Disabled == false && x.TileStorageId == tileStorageId.Value)
                .ToList() ?? new List<NotificationPluginTileConfig>();
        }

        private async Task<Dictionary<string, string>> GetTileConfiguration(NewDataEvent eventBody)
        {
            var tile = await _tileRepository.GetTile<TileEntity>(eventBody.TileId);
            var tileConfiguration = tile.TileConfiguration?.ToDictionary(x => x.Key, x => x.Value.ToString()) ?? new Dictionary<string, string>();
            return tileConfiguration;
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        private async Task SendNotificationAsync(NewDataEvent eventBody, INotificationPlugin plugin, NotificationPluginTileConfig pluginConfigurationForTile, IReadOnlyDictionary<string, string> tileConfiguration, CancellationToken cancellationToken)
        {
            try
            {
                var pluginConfiguration = pluginConfigurationForTile.Configuration.ToDictionary(x => x.Key, x => x.Value);
                switch (eventBody.TileId.Type)
                {
                    case TileType.Metric:
                        await ((MetricNotificationPlugin)plugin).PerformNotificationAsync(eventBody.TileId, eventBody.TileValue as MetricValue, pluginConfiguration, tileConfiguration, cancellationToken);
                        break;
                    case TileType.Weather:
                        await ((WeatherNotificationPlugin)plugin).PerformNotificationAsync(eventBody.TileId, eventBody.TileValue as WeatherValue, pluginConfiguration, tileConfiguration, cancellationToken);
                        break;
                    case TileType.Dual:
                        await ((DualNotificationPlugin)plugin).PerformNotificationAsync(eventBody.TileId, eventBody.TileValue as DualValue, pluginConfiguration, tileConfiguration, cancellationToken);
                        break;
                    case TileType.Integer:
                        await ((IntegerNotificationPlugin)plugin).PerformNotificationAsync(eventBody.TileId, eventBody.TileValue as IntegerValue, pluginConfiguration, tileConfiguration, cancellationToken);
                        break;
                    case TileType.HeartBeat:
                        await ((HeartBeatNotificationPlugin)plugin).PerformNotificationAsync(eventBody.TileId, eventBody.TileValue as HeartBeatValue, pluginConfiguration, tileConfiguration, cancellationToken);
                        break;
                    default:
                        _logger.LogWarning($"Notification plugin for Tile Type {eventBody.TileId.Type} is not supported.");
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Notification plugin {plugin.GetType()} throw exception: {ex.Message}. Inner exception: {ex.InnerException?.Message}");
            }
        }
    }
}
