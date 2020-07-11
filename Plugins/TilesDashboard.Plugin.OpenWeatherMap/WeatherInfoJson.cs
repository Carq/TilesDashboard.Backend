using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.OpenWeatherMap
{
    public class WeatherInfoJson
    {
        [JsonPropertyName("main")]
        public MainJson Main { get; set; }

        public decimal Temperature => Main.Temperature;

        public decimal Humidity => Main.Humidity;
    }
}
