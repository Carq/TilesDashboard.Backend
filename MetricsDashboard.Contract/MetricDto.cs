using System;
using MetricsDashboard.Contract.Enums;

namespace MetricsDashboard.Contract
{
    public class MetricDto
    {
        public decimal Current { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public decimal Limit { get; set; }

        public decimal? Wish { get; set; }

        public decimal? Goal { get; set; }

        public MetricType Type { get; set; }
    }
}