using TilesDashboard.Core.Type.Enums;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;

namespace TilesDashboard.PluginBase.MetricPlugin
{
    public abstract class HeartBeatPluginBase : DataPluginBase<HeartBeatData>
    {
        protected HeartBeatPluginBase(IPluginConfigProvider pluginConfigProvider)
           : base(pluginConfigProvider)
        {
        }

        public override TileType TileType => TileType.HeartBeat;
    }
}
