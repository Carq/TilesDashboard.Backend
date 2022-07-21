using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Crypto.Dto
{
    public class LtcPlnDto
    {
        [JsonPropertyName("rate")]
        public string Rate { get; set; }
    }
}
