using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.Plugin.OpenWeatherMap
{
    public class OpenWeatherMapPlugin : WeatherPluginBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _apiKey;

        private string _cityId;

        private string _cronSchedule;

        public OpenWeatherMapPlugin(IPluginConfigProvider pluginConfigProvider)
            : base(pluginConfigProvider)
        {
        }

        public override string TileName => "Gliwice";

        public override string CronSchedule => "*/10 * * * * *";

        public override Task InitializeAsync()
        {
            _apiKey = ConfigProvider.GetConfigEntry("OpenWeatherMapPlugin:ApiKey");
            _cityId = ConfigProvider.GetConfigEntry("OpenWeatherMapPlugin:CityId");
            _cronSchedule = ConfigProvider.GetConfigEntry("OpenWeatherMapPlugin:CronSchedule");
            return Task.CompletedTask;
        }

        public override async Task<WeatherData> GetDataAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?id={_cityId}&appid={_apiKey}&units=metric", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var info = JsonSerializer.Deserialize<WeatherInfoJson>(await response.Content.ReadAsStringAsync());
                return new WeatherData(info.Temperature, info.Humidity, Status.OK);
            }

            return WeatherData.Error($"Code: {response.StatusCode}");
        }
    }
}
