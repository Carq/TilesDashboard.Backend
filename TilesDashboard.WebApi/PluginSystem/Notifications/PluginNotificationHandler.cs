using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Contract.Events;
using TilesDashboard.Handy.Events;
using TilesDashboard.PluginSystem.Repositories;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        public async Task ExecuteAsync(NewDataEvent eventBody, CancellationToken cancellationToken)
        {
            var notificationPluginsConfigurations = await _pluginConfigRepository.GetEnabledNotificationPluginsConfiguration(eventBody.TileId.Type, cancellationToken);
            var notificationPlugins = _notificationPluginRepository.FindNotificationPlugins(notificationPluginsConfigurations.Select(x => x.PluginName).ToList());
            var tileStorageId = await _tileRepository.CheckIfExist(eventBody.TileId);
            _logger.LogDebug($"Found {notificationPlugins.Count} notification plugins for {eventBody.TileId}.");
            foreach (var plugin in notificationPlugins)
            {
                var notificationConfigurationsForPlugin = notificationPluginsConfigurations.SingleOrDefault(x => x.PluginName == plugin.UniquePluginName).NotificationPluginTileConfigs;
                foreach (var pluginTileConfiguration in notificationConfigurationsForPlugin.Where(y => y.TileStorageId == tileStorageId.Value))
                {
                    try
                    {
                        await ((dynamic)plugin).PerformNotificationAsync(
                            eventBody.TileId,
                            (dynamic)eventBody.TileValue,
                            pluginTileConfiguration.Configuration,
                            cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Notification plugin {plugin.GetType()} throw exception: {ex.Message}. Inner exception: {ex.InnerException?.Message}");
                    }
                }
            }
        }
    }
}
