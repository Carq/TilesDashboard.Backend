using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Type;

namespace TilesDashboard.PluginBase
{
    public abstract class PluginBase<TResult> : IPlugin
        where TResult : Result
    {
        public PluginBase(IPluginConfigProvider configProvider)
        {
            ConfigProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));
        }

        public PluginBase()
        {
        }

        /// <inheritdoc/>
        public abstract string TileName { get; }

        /// <inheritdoc/>
        public abstract TileType TileType { get; }

        /// <inheritdoc/>
        public abstract string CronSchedule { get; }

        /// <summary>
        /// Give access to config entries. Config Provider will be injected by PluginSystem.
        /// </summary>
        public IPluginConfigProvider ConfigProvider { get; }

        public abstract Task<TResult> GetDataAsync(CancellationToken cancellation = default);

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
