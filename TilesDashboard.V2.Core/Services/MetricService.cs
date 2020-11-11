using System;
using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class MetricService : IMetricService
    {
        private readonly ITileRepository _metricRepository;

        private readonly IDateTimeOffsetProvider _dateTimeProvider;

        public MetricService(ITileRepository metricRepository, IDateTimeOffsetProvider dateTimeProvider)
        {
            _metricRepository = metricRepository ?? throw new ArgumentNullException(nameof(metricRepository));
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<MetricTile> GetTile(TileId tileId)
        {
            var test = await _metricRepository.GetTile<MetricTile>(tileId);
            return test;
        }

        public async Task RecordValue(TileId tileId, MetricType metricType, decimal newValue)
        {
            var metric = await _metricRepository.GetTile<MetricTile>(tileId);
            MetricValue newMetricValue = metric.MetricType switch
            {
                MetricType.Percentage => new PercentageMetricValue(newValue, _dateTimeProvider.Now),
                MetricType.Time => new TimeMetricValue(newValue, _dateTimeProvider.Now),
                MetricType.Money => new MoneyMetricValue(newValue, _dateTimeProvider.Now),
                MetricType.Unspecified => throw new NotSupportedException(),
                _ => throw new NotSupportedException()
            };

            _metricRepository.RecordValue(tileId, newMetricValue);
        }
    }
}
