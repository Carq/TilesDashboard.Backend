using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.V2.Core.Entities.Metric
{
    public class MetricConfiguration
    {
        public MetricConfiguration(IDictionary<string, object> configurationAsDictionary)
        {
            Limit = configurationAsDictionary.GetValue<decimal>(nameof(Limit));
            Wish = configurationAsDictionary.GetValueOrNull<decimal>(nameof(Wish));
            Goal = configurationAsDictionary.GetValueOrNull<decimal>(nameof(Goal));
            Unit = configurationAsDictionary.GetValueOrDefault<string>(nameof(Unit));
            LowerIsBetter = configurationAsDictionary.GetValueOrDefault<bool>(nameof(LowerIsBetter));
            MetricType = configurationAsDictionary.GetValue<MetricType>(nameof(MetricType));
        }

        public decimal Limit { get; private set; }

        public decimal? Wish { get; private set; }

        public decimal? Goal { get; private set; }

        public string Unit { get; private set; }

        public bool LowerIsBetter { get; private set; }

        public MetricType MetricType { get; private set; }
    }
}