using System;

namespace TilesDashboard.V2.Core.Entities
{
    public class TileValue : ITileValue
    {
        protected TileValue(DateTimeOffset addedOn)
        {
            AddedOn = addedOn;
        }

        /// <summary>
        /// Date when specif tile value has been added.
        /// </summary>
        public DateTimeOffset AddedOn { get; private set; }
    }
}
