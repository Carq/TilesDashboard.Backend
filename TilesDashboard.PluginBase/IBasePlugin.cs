using System.Threading.Tasks;
using TilesDashboard.Core.Type;

namespace TilesDashboard.PluginBase
{
    public interface IBasePlugin
    {
        /// <summary>
        /// Tile Id contains Tile Name and Tile Type that define the uniqueness of the Tile.
        /// </summary>
        abstract TileId TileId { get; }

        /// <summary>
        /// Type of plugin. Data or Notification.
        /// </summary>
        abstract PluginType Type { get; }

        Task InitializeAsync();
    }
}
