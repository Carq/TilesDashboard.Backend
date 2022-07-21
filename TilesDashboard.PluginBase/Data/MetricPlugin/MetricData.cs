using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.PluginBase.Data.MetricPlugin
{
    public class MetricData : PluginDataResult
    {
        public MetricData(Status status) : base(status)
        {
        }

        public MetricData(decimal value, MetricType metric, Status status) : base(status)
        {
            Value = value;
            MetricType = metric;
        }

        public static MetricData Error(string errorMessage) => new MetricData(Status.Error).WithErrorMessage(errorMessage) as MetricData;

        public static MetricData NoUpdate() => new MetricData(Status.NoUpdate);

        public decimal Value { get; }

        public MetricType MetricType { get; }

        public override string ToString()
        {
            return $"Value: {Value} - MetricType - {MetricType} - Status: {Status}";
        }
    }
}
