using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Azure.CodeCoverage.Dtos
{
    public class BuildListDto
    {
        [JsonPropertyName("value")]
        public IList<BuildDetailsDto> Value { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
