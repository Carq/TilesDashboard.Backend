using System;

namespace MetricsDashboard.Dto
{
    public class Metric
    {
        public DateTimeOffset AddedOn { get; set; }

        public object Value { get; set; }
    }
}