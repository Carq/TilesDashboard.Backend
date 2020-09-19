using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Contract.Events;
using TilesDashboard.Handy.Events;

namespace TilesDashboard.WebApi.PluginSystem.Notifications
{
    public class PluginNotificationEventHandler : IEventHandler<NewDataEvent>
    {
        private readonly ILogger<PluginNotificationEventHandler> _logger;

        private readonly INotificationPluginRepository _notificationPluginRepository;

        public PluginNotificationEventHandler(INotificationPluginRepository notificationPluginRepository, ILogger<PluginNotificationEventHandler> logger)
        {
            _notificationPluginRepository = notificationPluginRepository ?? throw new ArgumentNullException(nameof(notificationPluginRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        public async Task ExecuteAsync(NewDataEvent eventBody, CancellationToken cancellationToken)
        {
            var notificationPlugins = _notificationPluginRepository.FindNotificationPlugins(eventBody.TileId);
            _logger.LogDebug($"Found {notificationPlugins.Count} notification plugins for {eventBody.TileId}.");
            foreach (var plugin in notificationPlugins)
            {
                try
                {
                    await plugin.PerformNotificationAsync(eventBody.NewValue, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Notification plugin {plugin.GetType()} throw exception: {ex.Message}. Inner exception: {ex.InnerException?.Message}");
                }
            }
        }
    }
}
