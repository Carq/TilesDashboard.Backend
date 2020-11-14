using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.MetricPlugin
{
    public abstract class MetricPluginBase : DataPluginBase<MetricData>
    {
        protected MetricPluginBase(IPluginConfigProvider pluginConfigProvider)
           : base(pluginConfigProvider)
        {
        }

        public override TileType TileType => TileType.Metric;
    }
}
