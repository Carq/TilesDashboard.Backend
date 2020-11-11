using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.V2.Core.Services
{
    public interface IMetricService
    {
        Task<MetricTile> GetTile(TileId tileId);

        Task RecordValue(TileId tileId, MetricType metricType, decimal newValue);
    }
}