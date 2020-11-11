using System;
using System.Collections.Generic;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.V2.Core.Entities
{
    public class TileId : ValueObject
    {
        public TileId(string tileName, TileType tileType)
        {
            Name = tileName ?? throw new ArgumentNullException(nameof(tileName));
            Type = tileType != TileType.Undefined ? tileType : throw new ArgumentException("TileType cannot be Undefined", nameof(tileType));
        }

        public string Name { get; private set; }

        public TileType Type { get; private set; }

        public override string ToString()
        {
            return $"{Name} {Type}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Type;
        }
    }
}
