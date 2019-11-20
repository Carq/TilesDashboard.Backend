using System;

namespace MetricsDashboard.WebApi.Tools
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now { get; }
    }
}