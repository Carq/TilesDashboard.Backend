using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TileDashboard.TestNotificationPlugins
{
    public class WeatherConsoleNotificationPlugin : NotificationPluginBase
    {
        public WeatherConsoleNotificationPlugin(IPluginConfigProvider pluginConfigProvider)
         : base(pluginConfigProvider)
        {
        }

        public override TileId TileId => new TileId("Gliwice", TileType.Weather);

        public override Task PerformNotificationAsync(object newData, CancellationToken cancellationToken)
        {
            Console.WriteLine($"New notification from {TileId}");
            return Task.CompletedTask;
        }
    }
}
