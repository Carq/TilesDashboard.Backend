using System;
using TilesDashboard.Core.Type;

namespace TilesDashboard.PluginBase.MetricPlugin
{
    public class MetricData : Result
    {
        public MetricData(Status status) : base(status)
        {
        }

        public MetricData(decimal percentageCodeCoverage, MetricType metric, Status status) : base(status)
        {
            PercentageCodeCoverage = percentageCodeCoverage;
            MetricType = metric;
        }

        public static MetricData Error(string errorMessage) => new MetricData(Status.Error).WithErrorMessage(errorMessage) as MetricData;

        public decimal PercentageCodeCoverage { get; }

        public MetricType MetricType { get; }
    }
}
