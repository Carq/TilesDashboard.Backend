using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase
{
    public interface IPlugin
    {
        string UniquePluginName { get; }

        TileType TileType { get; }

        PluginType PluginType { get; }
    }
}
