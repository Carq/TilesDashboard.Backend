using System;
using System.Collections.Generic;
using TilesDashboard.Core.Type.Enums;

namespace TilesDashboard.Core.Type
{
    public class TileId : ValueObject
    {
        public TileId(string tileName, TileType tileType)
        {
            TileName = tileName ?? throw new ArgumentNullException(nameof(tileName));
            TileType = tileType != TileType.Undefined ? tileType : throw new ArgumentException("TileType cannot be Undefined", nameof(tileType));
        }

        public string TileName { get; }

        public TileType TileType { get; }

        public override string ToString()
        {
            return $"{TileName} {TileType}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TileName;
            yield return TileType;
        }
    }
}
