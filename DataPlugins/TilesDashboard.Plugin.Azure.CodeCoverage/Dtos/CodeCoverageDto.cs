using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Azure.CodeCoverage.Dtos
{
    public class CodeCoverageDto
    {
        [JsonPropertyName("coverageData")]
        public IList<CoverageDataDto> CoverageData { get; set; }
    }
}
