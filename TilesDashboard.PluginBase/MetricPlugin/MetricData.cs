using System;

namespace TilesDashboard.PluginBase.MetricPlugin
{
    public class MetricData : Result
    {
        public MetricData(Status status) : base(status)
        {
        }

        public MetricData(decimal percentageCodeCoverage, Status status) : base(status)
        {
            PercentageCodeCoverage = percentageCodeCoverage;
        }

        public static MetricData Error(string errorMessage) => new MetricData(Status.Error).WithErrorMessage(errorMessage) as MetricData;

        public decimal PercentageCodeCoverage { get; }
    }
}
