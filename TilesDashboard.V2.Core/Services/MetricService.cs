using System;
using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class MetricService : TileBaseService, IMetricService
    {
        private readonly IDateTimeOffsetProvider _dateTimeProvider;

        public MetricService(IDateTimeOffsetProvider dateTimeProvider, ITileRepository tileRepository)
            : base(tileRepository)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<MetricTile> GetMetricTile(TileId tileId) => await GetTile<MetricTile>(tileId);

        public async Task RecordValue(TileId tileId, MetricType metricType, decimal newValue)
        {
            var metric = await GetTile<MetricTile>(tileId);
            MetricValue newMetricValue = metric.MetricType switch
            {
                MetricType.Percentage => new PercentageMetricValue(newValue, _dateTimeProvider.Now),
                MetricType.Time => new TimeMetricValue(newValue, _dateTimeProvider.Now),
                MetricType.Money => new MoneyMetricValue(newValue, _dateTimeProvider.Now),
                MetricType.Unspecified => throw new NotSupportedException(),
                _ => throw new NotSupportedException()
            };

            TileRepository.RecordValue(tileId, newMetricValue);
        }
    }
}
