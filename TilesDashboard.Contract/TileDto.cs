using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Contract
{
    public class TileDto
    {
        public string Name { get; set; }

        public TileTypeDto TileType { get; set; }

        public object TileData { get; set; }
    }
}