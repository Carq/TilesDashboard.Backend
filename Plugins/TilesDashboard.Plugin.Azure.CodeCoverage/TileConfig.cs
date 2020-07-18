using System;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class TileConfig
    {
        public TileConfig(string tileName, string buildDefinition)
        {
            TileName = tileName ?? throw new ArgumentNullException(nameof(tileName));
            BuildDefinition = buildDefinition ?? throw new ArgumentNullException(nameof(buildDefinition));
        }

        public string TileName { get; }

        public string BuildDefinition { get; }
    }
}
