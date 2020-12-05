using System.Collections.Generic;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Exceptions;

namespace TilesDashboard.V2.Core.Storage
{
    public class TileStorageId : ValueObject
    {
        public TileStorageId(string value)
        {
            Value = string.IsNullOrWhiteSpace(value) ? throw new ValidationException("Tile Storage Id cannot be null or empty") : value;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
