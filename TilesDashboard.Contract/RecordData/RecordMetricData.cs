using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Contract.RecordData
{
    public class RecordMetricData<TValue>
    {
        public TValue Value { get; set; }

        public MetricTypeDto Type { get; set; }
    }
}
