using TilesDashboard.Core.Type.Enums;
using TilesDashboard.PluginBase.Data;

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
