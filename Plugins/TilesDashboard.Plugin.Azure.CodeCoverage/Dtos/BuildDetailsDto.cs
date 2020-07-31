using System;
using System.Text.Json.Serialization;

namespace TilesDashboard.Plugin.Azure.CodeCoverage.Dtos
{
    public class BuildDetailsDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("startTime")]
        public DateTimeOffset StartTime { get; set; }
    }
}
