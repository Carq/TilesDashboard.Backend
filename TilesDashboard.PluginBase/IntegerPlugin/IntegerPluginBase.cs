using TilesDashboard.Contract.Enums;
using TilesDashboard.PluginBase.IntegerPlugin;

namespace TilesDashboard.PluginBase.MetricPlugin
{
    public abstract class IntegerPluginBase : PluginBase<IntegerData>
    {
        protected IntegerPluginBase(IPluginConfigProvider pluginConfigProvider)
           : base(pluginConfigProvider)
        {
        }

        public override TileType TileType => TileType.Integer;
    }
}
