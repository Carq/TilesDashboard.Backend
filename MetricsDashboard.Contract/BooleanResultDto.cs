using System;

namespace MetricsDashboard.Contract
{
    public class BooleanResultDto
    {
        public bool IsSuccess { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}
