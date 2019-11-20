using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.WebApi.Entities;

namespace MetricsDashboard.WebApi.Services
{
    public interface IMetricReactiveService
    {
        Task<IList<MetricHistory>> GetMetricHistoryAsync(int metricId, CancellationToken token);
    }
}