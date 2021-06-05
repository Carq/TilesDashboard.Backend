using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Crypto.Dto
{
    public class CryptoItemDto
    {
        [JsonPropertyName("LTC-PLN")]
        public LtcPlnDto LtcPln { get; set; }
    }
}
