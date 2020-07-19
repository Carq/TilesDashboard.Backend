using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Type;

namespace TilesDashboard.Core.Domain.Services
{
    public interface IMetricService
    {
        Task RecordMetricDataAsync(string tileName, MetricType metricType, decimal currentValue, CancellationToken cancellationToken);

        Task<IList<MetricData>> GetMetricRecentDataAsync(string tileName, int amountOfData, CancellationToken token);
    }
}
