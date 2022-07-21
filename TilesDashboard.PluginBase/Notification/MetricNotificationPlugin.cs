using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.PluginBase.Notification
{
    public abstract class MetricNotificationPlugin : NotificationPluginBase<MetricValue>
    {
        public override TileType TileType => TileType.Metric;
    }
}
