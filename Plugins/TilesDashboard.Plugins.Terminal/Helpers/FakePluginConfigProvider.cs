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
                case "AzureCodeCoveragePluginBe:Organization":
                    return "sporttecag";
                case "AzureCodeCoveragePluginBe:Project":
                    return "sporttec";
                case "AzureCodeCoveragePluginBe:BuildDefinition":
                    return "12";
                case "AzureCodeCoveragePluginBe:PersonalAccessToken":
                    return "hseegbdb3ceshchzrviqoee5y4jpqxglfhiolglsjzkh2jbdb2ua";
                 case "AzureCodeCoveragePluginBe:TileName":
                    return "";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
