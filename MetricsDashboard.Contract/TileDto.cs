using MetricsDashboard.Contract.Enums;

namespace MetricsDashboard.Contract
{
    public class TileDto
    {
        public string Name { get; set; }

        public TileType TileType { get; set; }

        public object TileData { get; set; }

        public static TileDto CreateMetric(string name, MetricDto metric)
        {
            return new TileDto
            {
                Name = name,
                TileType = TileType.Metric,
                TileData = metric,
            };
        }
    }
}