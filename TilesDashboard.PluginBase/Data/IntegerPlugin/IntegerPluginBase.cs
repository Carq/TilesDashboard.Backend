using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.IntegerPlugin
{
    public abstract class IntegerPluginBase : DataPluginBase<IntegerData>
    {
        protected IntegerPluginBase(IPluginConfigProvider pluginConfigProvider)
           : base(pluginConfigProvider)
        {
        }

        public override TileType TileType => TileType.Integer;
    }
}
