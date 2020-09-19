using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Type;
using TilesDashboard.Core.Type.Enums;

namespace TilesDashboard.PluginBase.Data
{
    public abstract class DataPluginBase<TResult> : IDataPlugin
        where TResult : Result
    {
        public DataPluginBase(IPluginConfigProvider configProvider)
        {
            ConfigProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));
        }

        public DataPluginBase()
        {
        }

        /// <summary>
        /// Tile Name which is used to update Tile in database and to display tile name on frontend.
        /// </summary>
        public abstract string TileName { get; }

        /// <summary>
        /// Tile type.
        /// </summary>
        public abstract TileType TileType { get; }

        /// <inheritdoc/>
        public abstract string CronSchedule { get; }

        /// <summary>
        /// Give access to config entries. Config Provider will be injected by PluginSystem.
        /// </summary>
        public IPluginConfigProvider ConfigProvider { get; }

        public TileId TileId => new TileId(TileName, TileType);

        public abstract Task<TResult> GetDataAsync(CancellationToken cancellation = default);

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
