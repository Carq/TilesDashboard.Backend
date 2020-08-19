using TilesDashboard.Contract.Enums;
using TilesDashboard.PluginBase.IntegerPlugin;

namespace TilesDashboard.PluginBase.MetricPlugin
{
    public abstract class HeartBeatPluginBase : PluginBase<HeartBeatData>
    {
        protected HeartBeatPluginBase(IPluginConfigProvider pluginConfigProvider)
           : base(pluginConfigProvider)
        {
        }

        public override TileType TileType => TileType.HeartBeat;
    }
}
