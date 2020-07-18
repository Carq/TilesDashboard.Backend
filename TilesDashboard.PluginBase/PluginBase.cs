using System;
using System.Threading;
using System.Threading.Tasks;

namespace TilesDashboard.PluginBase
{
    public abstract class PluginBase<TResult>
        where TResult : Result
    {
        public PluginBase(IPluginConfigProvider configProvider)
        {
            ConfigProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));
        }

        public PluginBase()
        {
        }

        /// <summary>
        /// Tile Name which is used to update Tile in database and to display tile name on frontend.
        /// </summary>
        public abstract string TileName { get; }

        /// <summary>
        /// Schedule execution of GetDataAsync(). https://crontab.cronhub.io/
        /// </summary>
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
