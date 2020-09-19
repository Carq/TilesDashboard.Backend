namespace TilesDashboard.PluginBase.Data
{
    public interface IDataPlugin : IBasePlugin
    {
        /// <summary>
        /// Schedule execution of GetDataAsync(). https://crontab.cronhub.io/
        /// </summary>
        public abstract string CronSchedule { get; }

        PluginType IBasePlugin.Type => PluginType.Data;
    }
}
