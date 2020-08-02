using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Crypto
{
    public class CryptoTickerDto
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("items")]
        public CryptoItemDto Items { get; set; }
    }
}
