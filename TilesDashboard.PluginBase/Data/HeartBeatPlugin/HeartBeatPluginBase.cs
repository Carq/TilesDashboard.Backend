using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

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
