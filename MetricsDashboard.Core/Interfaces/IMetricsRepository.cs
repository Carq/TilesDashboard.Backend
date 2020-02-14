using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;
using MetricsDashboard.Core.Entities;

namespace MetricsDashboard.Core.Interfaces
{
    public interface IMetricsRepository
    {
        Task<IEnumerable<MetricType>> GetAvailableMetricsAsync(CancellationToken cancellationToken);

        Task<Entities.MetricBase> GetLatestAsync(MetricKind metricKind, string metricName, CancellationToken cancellationToken);
    }
}