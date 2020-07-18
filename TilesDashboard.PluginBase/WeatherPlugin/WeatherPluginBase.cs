namespace TilesDashboard.PluginBase.WeatherPlugin
{
    public abstract class BaseWeatherPlugin : PluginBase<WeatherData>
    {
        protected BaseWeatherPlugin(IPluginConfigProvider pluginConfigProvider)
            : base(pluginConfigProvider)
        {
        }

        protected BaseWeatherPlugin()
        {
        }
    }
}
