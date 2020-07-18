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
                case "AzureCodeCoveragePlugin:Organization":
                    return "";
                case "AzureCodeCoveragePlugin:Project":
                    return "";
                case "AzureCodeCoveragePlugin:BuildDefinition":
                    return "";
                case "AzureCodeCoveragePlugin:PersonalAccessToken":
                    return "";
                 case "AzureCodeCoveragePlugin:TileName":
                    return "";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
