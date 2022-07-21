using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Integer;

namespace TilesDashboard.PluginBase.Notification
{
    public abstract class IntegerNotificationPlugin : NotificationPluginBase<IntegerValue>
    {
        public override TileType TileType => TileType.Integer;
    }
}
