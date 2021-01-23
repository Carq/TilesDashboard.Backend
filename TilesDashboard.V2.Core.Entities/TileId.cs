using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Exceptions;

namespace TilesDashboard.V2.Core.Entities
{
    public class TileId : ValueObject
    {
        public TileId(string tileName, TileType tileType)
        {
            Name = string.IsNullOrWhiteSpace(tileName) ? throw new ValidationException("Tile name cannot be empty") : tileName;
            Type = tileType.IsUndefined() ? throw new ValidationException("TileType cannot be Undefined") : tileType;
        }

        public string Name { get; private set; }

        public TileType Type { get; private set; }

        public override string ToString()
        {
            return $"{Name} {Type}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name.ToUpperInvariant();
            yield return Type;
        }
    }
}
