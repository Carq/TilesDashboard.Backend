using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.HeartBeat;

namespace TilesDashboard.PluginBase.Notification
{
    public abstract class HeartBeatNotificationPlugin : NotificationPluginBase<HeartBeatValue>
    {
        public override TileType TileType => TileType.HeartBeat;
    }
}
