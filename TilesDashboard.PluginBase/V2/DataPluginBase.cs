using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.V2
{
    public abstract class DataPluginBase<TResult> : IDataPlugin
        where TResult : Result
    {
        /// <summary>
        /// Unique Plugin Name which is used to get plugin config from storage.
        /// </summary>
        public abstract string UniquePluginName { get; }

        /// <summary>
        /// Tile type.
        /// </summary>
        public abstract TileType TileType { get; }

        public abstract Task<TResult> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default);
    }
}
