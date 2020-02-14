using System;
using MetricsDashboard.Contract.Attributes;

namespace MetricsDashboard.Contract
{
    public enum MetricKind
    {
        [MetricKindType(typeof(void))]
        Undefined = 0,

        [MetricKindType(typeof(decimal))]
        Percentage,

        [MetricKindType(typeof(decimal))]
        Money,

        [MetricKindType(typeof(TimeSpan))]
        Time,
    }
}