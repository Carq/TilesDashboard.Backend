using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Azure.CodeCoverage.Dtos
{
    public class CoverageDataDto
    {
        [JsonPropertyName("coverageStats")]
        public IList<CoverageStatsDto> CoverageStats { get; set; }
    }
}
