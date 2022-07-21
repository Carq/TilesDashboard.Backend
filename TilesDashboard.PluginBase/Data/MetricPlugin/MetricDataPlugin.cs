using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.MetricPlugin
{
    public abstract class MetricDataPlugin : DataPluginBase<MetricData>
    {
        public override TileType TileType => TileType.Metric;
    }
}
