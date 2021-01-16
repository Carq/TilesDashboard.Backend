using TilesDashboard.PluginBase.Data.HeartBeatPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.V2
{
    public abstract class HeartBeatPluginBase : DataPluginBase<HeartBeatData> 
    {
        public override TileType TileType => TileType.HeartBeat;
    }
}
