using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.WeatherPlugin;

namespace TilesDashboard.Plugin.OpenWeatherMap
{
    public class OpenWeatherMapPlugin : WeatherPluginBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public override string UniquePluginName => $"TileCorePlugins.{nameof(OpenWeatherMapPlugin)}";

        public override async Task<WeatherData> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellationToken = default)
        {
            var cityId = pluginConfiguration["CityId"];
            var apiKey = pluginConfiguration["ApiKey"];

            if (string.IsNullOrWhiteSpace(cityId) || string.IsNullOrWhiteSpace(apiKey))
            {
                WeatherData.Error("CityId or ApiKey is not provided");
            }

            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?id={cityId}&appid={apiKey}&units=metric", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var info = JsonSerializer.Deserialize<WeatherInfoJson>(await response.Content.ReadAsStringAsync());
                if (info != null) return new WeatherData(info.Temperature, info.Humidity, Status.OK);
            }

            return WeatherData.Error($"Code: {response.StatusCode}");
        }
    }
}
