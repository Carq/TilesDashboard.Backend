using System;

namespace TilesDashboard.Handy.Tools
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now { get; }
    }
}