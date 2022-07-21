using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.DualPlugin
{
    public abstract class DualPluginBase : DataPluginBase<DualData> 
    {
        public override TileType TileType => TileType.Dual;
    }
}
