using System;

namespace MetricsDashboard.Contract
{
    public class StatusDto
    {
        public bool Current { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}
