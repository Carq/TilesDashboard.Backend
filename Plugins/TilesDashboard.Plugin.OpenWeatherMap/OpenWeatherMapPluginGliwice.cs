using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.Plugin.OpenWeatherMap
{
    public class OpenWeatherMapPluginGliwice : WeatherPluginBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _apiKey;

        private string _cityId;

        private string _cronSchedule;

        private string _tileName;

        private readonly string RootConfig = "OpenWeatherMapPluginGliwice";

        public OpenWeatherMapPluginGliwice(IPluginConfigProvider pluginConfigProvider)
            : base(pluginConfigProvider)
        {
        }

        public override string TileName => _tileName;

        public override string CronSchedule => _cronSchedule;

        public override Task InitializeAsync()
        {
            _apiKey = ConfigProvider.GetConfigEntry($"{RootConfig}:ApiKey");
            _cityId = ConfigProvider.GetConfigEntry($"{RootConfig}:CityId");
            _cronSchedule = ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");
            _tileName = ConfigProvider.GetConfigEntry($"{RootConfig}:TileName");

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
