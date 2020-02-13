using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.DataAccess.Entities;
using MetricsDashboard.Dto;

namespace MetricsDashboard.DataAccess.Interfaces
{
    public interface IMetricsRepository
    {
        Task<IMetric> GetLatestAsync(MetricKind metricKind, string metricName, CancellationToken cancellationToken);

        Task<IEnumerable<AvailableMetric>> GetAvailableMetricsAsync(CancellationToken cancellationToken);
    }
}