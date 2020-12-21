using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.V2
{
    public abstract class MetricPluginBase : DataPluginBase<MetricData>
    {
        public override TileType TileType => TileType.Metric;

    }
}
