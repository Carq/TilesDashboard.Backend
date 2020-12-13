using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.V2.Core.Services
{
    public interface IMetricService
    {
        Task<MetricTile> GetMetricTile(TileId tileId);

        Task RecordValue(TileId tileId, MetricType metricType, decimal newValue);

        Task RecordValue(StorageId storageId, MetricType metricType, decimal newValue);
    }
}