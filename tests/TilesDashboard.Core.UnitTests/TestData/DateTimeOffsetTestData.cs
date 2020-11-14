using System;
using System.Globalization;

namespace TilesDashboard.Core.UnitTests.TestData
{
    public static class DateTimeOffsetTestData
    {
        public static readonly DateTimeOffset April02Year2020 = DateTimeOffset.ParseExact("2020-04-02", "yyyy-MM-dd", CultureInfo.InvariantCulture);

        public static readonly DateTimeOffset June28Year2020At0639 = DateTimeOffset.ParseExact("2020-06-28 06:39:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        public static readonly DateTimeOffset June28Year2020At102211 = DateTimeOffset.ParseExact("2020-06-28 10:22:11", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
}
