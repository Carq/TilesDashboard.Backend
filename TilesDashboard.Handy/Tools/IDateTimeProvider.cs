using System;

namespace TilesDashboard.Handy.Tools
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}