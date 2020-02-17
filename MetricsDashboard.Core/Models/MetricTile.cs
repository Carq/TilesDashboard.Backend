using System.Collections.Generic;
using System.Linq;
using MetricsDashboard.Contract;
using MetricsDashboard.Contract.Enums;
using MetricsDashboard.Core.Entities;

namespace MetricsDashboard.Core.Models
{
    public class MetricTile : ITile
    {
        private readonly string _name;

        private readonly MetricSettingsEntity _metricSettings;

        private readonly MetricEntity<decimal> _latest;

        public MetricTile(
            string name,
            MetricSettingsEntity settings,
            IEnumerable<MetricEntity<decimal>> metrics)
        {
            _name = name;
            _metricSettings = settings;
            _latest = metrics.OrderByDescending(m => m.AddedOn).First();
        }

        public TileDto ToDto()
        {
            return new TileDto
            {
                Name = _name,
                TileType = TileType.Metric,
                TileData = new MetricDto
                {
                    Goal = _metricSettings.Goal,
                    Limit = _metricSettings.Limit,
                    Wish = _metricSettings.Wish,
                    Type = _metricSettings.MetricType,
                    Current = _latest.Value,
                    LastUpdated = _latest.AddedOn,
                },
            };
        }
    }
}
