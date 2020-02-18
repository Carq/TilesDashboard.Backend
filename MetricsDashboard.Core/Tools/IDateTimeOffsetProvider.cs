using System;

namespace MetricsDashboard.Core.Tools
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now { get; }
    }
}