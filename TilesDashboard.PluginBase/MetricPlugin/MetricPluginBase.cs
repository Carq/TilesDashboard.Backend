using TilesDashboard.Core.Type;

namespace TilesDashboard.PluginBase.MetricPlugin
{
    public abstract class MetricPluginBase : PluginBase<MetricData>
    {
        protected MetricPluginBase(IPluginConfigProvider pluginConfigProvider)
           : base(pluginConfigProvider)
        {
        }

        public override TileType TileType => TileType.Metric;
    }
}
