using System.Collections.Generic;
using TilesDashboard.V2.Core.Entities.Exceptions;

namespace TilesDashboard.V2.Core.Entities
{
    public class StorageId : ValueObject
    {
        public const int Length = 24;

        public StorageId(string value)
        {
            Value = Validate(value);
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Id cannot be empty.");
            }

            if (value.Length != Length)
            {
                throw new ValidationException($"Id must be {Length} characters long.");
            }

            return value;
        }
    }
}
