using System;

namespace TilesDashboard.Contract.RecordData
{
    public class RecordDualValue
    {
        public decimal Primary { get; set; }

        public decimal Secondary { get; set; }

        public DateTimeOffset? OccurredOn { get; set; }
    }
}
