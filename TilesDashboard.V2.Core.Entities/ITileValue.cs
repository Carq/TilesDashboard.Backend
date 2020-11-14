using System;

namespace TilesDashboard.V2.Core.Entities
{
    public interface ITileValue
    {
        /// <summary>
        /// Date when specif tile value has been added.
        /// </summary>
        DateTimeOffset AddedOn { get; }
    }
}
