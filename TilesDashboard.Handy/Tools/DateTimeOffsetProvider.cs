using System;

namespace TilesDashboard.Handy.Tools
{
    public class DateTimeOffsetProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
