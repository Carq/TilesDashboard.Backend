using System;

namespace MetricsDashboard.Contract
{
    public class MetricDto
    {
        public DateTimeOffset AddedOn { get; set; }

        public object Value { get; set; }
    }
}