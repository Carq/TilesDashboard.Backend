using MetricsDashboard.Contract.Enums;

namespace MetricsDashboard.Contract
{
    public class TileDto
    {
        public string Name { get; set; }

        public TileType TileType { get; set; }

        public object TileData { get; set; }
    }
}