using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TileDashboard.TestNotificationPlugins
{
    public class WeatherConsoleNotificationPlugin : NotificationPluginBase<WeatherValue>
    {
        public override string UniquePluginName => nameof(WeatherConsoleNotificationPlugin);

        public override TileType TileType => TileType.Weather;

        public override Task PerformNotificationAsync(TileId tileId, WeatherValue newData, IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            Console.WriteLine($"New notification from {tileId}");
            return Task.CompletedTask;
        }
    }
}
