using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.V2
{
    public interface IDataPlugin
    {
        abstract string UniquePluginName { get; }

        TileType TileType { get; }

        PluginType PluginType => PluginType.Data;
    }
}
