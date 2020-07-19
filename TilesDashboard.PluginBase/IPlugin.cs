using System.Threading.Tasks;
using TilesDashboard.Core.Type;

namespace TilesDashboard.PluginBase
{
    public interface IPlugin
    {
        /// <summary>
        /// Tile Name which is used to update Tile in database and to display tile name on frontend.
        /// </summary>
        public abstract string TileName { get; }

        /// <summary>
        /// Schedule execution of GetDataAsync(). https://crontab.cronhub.io/
        /// </summary>
        public abstract string CronSchedule { get; }

        /// <summary>
        /// Tile type.
        /// </summary>
        public abstract TileType TileType { get; }

        Task InitializeAsync();
    }
}
