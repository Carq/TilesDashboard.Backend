namespace TilesDashboard.PluginBase.WeatherPlugin
{
    public abstract class WeatherPluginBase : PluginBase<WeatherData>
    {
        protected WeatherPluginBase(IPluginConfigProvider pluginConfigProvider)
            : base(pluginConfigProvider)
        {
        }

        protected WeatherPluginBase()
        {
        }
    }
}
