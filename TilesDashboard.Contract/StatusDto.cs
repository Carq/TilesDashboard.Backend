using System;

namespace TilesDashboard.Contract
{
    public class StatusDto
    {
        public bool Current { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}
