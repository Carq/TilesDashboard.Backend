using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.WebApi.Database;
using MetricsDashboard.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetricsDashboard.WebApi.Services
{
    public class MetricReactiveService : IMetricReactiveService
    {
        private readonly MetricsDbContext _dbContext;

        public MetricReactiveService(MetricsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IList<MetricHistory>> GetMetricHistoryAsync(int metricId, CancellationToken token)
        {
            return await _dbContext.Metrics.Include(x => x.History).Where(x => x.Id == metricId).SelectMany(x => x.History).ToListAsync(token);
        }
    }
}
