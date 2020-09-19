using System;
using System.Threading.Tasks;
using TilesDashboard.Core.Type;
using TilesDashboard.Core.Type.Enums;
using TilesDashboard.PluginBase.Notification;

namespace TileDashboard.TestNotificationPlugins
{
    public class WeatherConsoleNotification : NotificationPluginBase
    {
        public override TileId TileId => new TileId("Gliwice", TileType.Weather);

        public override Task PerformNotification()
        {
            Console.WriteLine($"New notification from {TileId}");
            return Task.CompletedTask;
        }
    }
}
