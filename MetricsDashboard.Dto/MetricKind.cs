using System;
using MetricsDashboard.Dto.Attributes;

namespace MetricsDashboard.Dto
{
    public enum MetricKind
    {
        [MetricKindType(typeof(decimal))]
        Percentage,

        [MetricKindType(typeof(decimal))]
        Money,

        [MetricKindType(typeof(TimeSpan))]
        Time,
    }
}