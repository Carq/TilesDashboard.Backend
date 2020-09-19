using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Crypto
{
    public class CryptoItemDto
    {
        [JsonPropertyName("LTC-PLN")]
        public LtcPlnDto LtcPln { get; set; }
    }
}
