using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;

namespace TilesDashboard.Core.Domain.Services
{
    public interface IMetricService
    {
        Task RecordMetricDataAsync(string tileName, MetricType metricType, decimal currentValue, CancellationToken token);

        Task<MetricData> GetMetricRecentDataAsync(string tileName, CancellationToken token);
    }
}
