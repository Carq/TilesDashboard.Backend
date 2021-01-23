using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.HeartBeatPlugin
{
    public abstract class HeartBeatPluginBase : DataPluginBase<HeartBeatData>
    {
        public override TileType TileType => TileType.HeartBeat;
    }
}
