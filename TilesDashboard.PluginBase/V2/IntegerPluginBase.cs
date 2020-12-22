using TilesDashboard.PluginBase.Data.IntegerPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.V2
{
    public abstract class IntegerPluginBase : DataPluginBase<IntegerData>
    {
        public override TileType TileType => TileType.Integer;
    }
}
