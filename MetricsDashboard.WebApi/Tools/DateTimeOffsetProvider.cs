using System;

namespace MetricsDashboard.WebApi.Tools
{
    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
