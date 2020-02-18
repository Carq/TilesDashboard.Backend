using System;

namespace MetricsDashboard.Core.Tools
{
    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
