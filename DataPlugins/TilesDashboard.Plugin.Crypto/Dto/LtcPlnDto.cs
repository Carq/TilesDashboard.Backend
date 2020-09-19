using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Crypto
{
    public class LtcPlnDto
    {
        [JsonPropertyName("rate")]
        public string Rate { get; set; }
    }
}
