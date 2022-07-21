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
    public class WeatherExceptionNotificationPlugin : NotificationPluginBase<WeatherValue>
    {
           public override string UniquePluginName => nameof(WeatherExceptionNotificationPlugin);

        public override TileType TileType => TileType.Weather;

        public override Task PerformNotificationAsync(TileId tileId, WeatherValue newData, IReadOnlyDictionary<string, string> pluginConfiguration, IReadOnlyDictionary<string, string> tileConfiguration, CancellationToken cancellation = default)
        {
            throw new NotImplementedException($"Exception from {tileId}");
        }
    }
}
