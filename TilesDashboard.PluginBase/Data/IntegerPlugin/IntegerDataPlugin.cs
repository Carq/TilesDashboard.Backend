using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.IntegerPlugin
{
    public abstract class IntegerDataPlugin : DataPluginBase<IntegerData>
    {
        public override TileType TileType => TileType.Integer;
    }
}
