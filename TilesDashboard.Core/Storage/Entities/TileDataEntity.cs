using System;

namespace TilesDashboard.Core.Entities
{
    public class TileData
    {
        protected TileData(DateTimeOffset addedOn)
        {
            AddedOn = addedOn;
        }

        public DateTimeOffset AddedOn { get; private set; }
    }
}