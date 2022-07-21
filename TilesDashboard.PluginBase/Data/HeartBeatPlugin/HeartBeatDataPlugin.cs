using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.HeartBeatPlugin
{
    public abstract class HeartBeatDataPlugin : DataPluginBase<HeartBeatData>
    {
        public override TileType TileType => TileType.HeartBeat;
    }
}
