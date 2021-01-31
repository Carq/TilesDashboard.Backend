using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data
{
    public interface IPlugin
    {
        abstract string UniquePluginName { get; }

        TileType TileType { get; }

        PluginType PluginType { get; }
    }
}
