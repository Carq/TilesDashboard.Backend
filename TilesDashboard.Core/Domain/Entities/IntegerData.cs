using System;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Entities
{
    public class IntegerData : TileData
    {
        public IntegerData(int value, DateTimeOffset addedOn)
            : base(addedOn)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }
}
