using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.OpenWeatherMap
{
    public class MainJson
    {
        [JsonPropertyName("temp")]
        public decimal Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public decimal Humidity { get; set; }
    }
}
