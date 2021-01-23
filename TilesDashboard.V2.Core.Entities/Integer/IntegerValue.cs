using System;

namespace TilesDashboard.V2.Core.Entities.Integer
{
    public class IntegerValue : TileValue
    {
        public IntegerValue(int value, DateTimeOffset addedOn) : base(addedOn)
        {
            Value = value;
        }

        public int Value { get; protected set; }
    }
}
