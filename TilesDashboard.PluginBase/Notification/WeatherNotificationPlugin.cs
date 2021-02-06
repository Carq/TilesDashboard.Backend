using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TilesDashboard.PluginBase.Notification
{
    public abstract class WeatherNotificationPlugin : NotificationPluginBase<WeatherValue>
    {
        public override TileType TileType => TileType.Weather;
    }
}
