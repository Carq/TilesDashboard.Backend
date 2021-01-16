using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.IntegerPlugin
{
    public abstract class IntegerPluginBase : DataPluginBase<IntegerData>
    {
        public override TileType TileType => TileType.Integer;
    }
}
