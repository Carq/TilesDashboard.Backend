using System;
using TilesDashboard.PluginBase;

namespace TilesDashboard.Plugins.Terminal.Helpers
{
    public class FakePluginConfigProvider : IPluginConfigProvider
    {
        public string GetConfigEntry(string configEntry)
        {
            switch (configEntry)
            {
                case "OpenWeatherMapPlugin:ApiKey":
                    return "";
                case "OpenWeatherMapPlugin:CityId":
                    return "";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
