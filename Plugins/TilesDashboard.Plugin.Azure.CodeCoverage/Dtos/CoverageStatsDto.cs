using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Azure.CodeCoverage.Dtos
{
    public class CoverageStatsDto
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("covered")]
        public int Covered { get; set; }
    }
}
