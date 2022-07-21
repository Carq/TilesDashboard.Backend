using TilesDashboard.V2.Core.Entities.Dual;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Notification
{
    public abstract class DualNotificationPlugin : NotificationPluginBase<DualValue>
    {
        public override TileType TileType => TileType.Dual;
    }
}
