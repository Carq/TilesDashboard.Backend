using System;

namespace TilesDashboard.Core.Tools
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now { get; }
    }
}