using System;

namespace TilesDashboard.Contract.RecordData
{
    public class RecordValueDto<TValue>
    {
        public TValue Value { get; set; }

        public DateTimeOffset? OccurredOn { get; set; }
    }
}
