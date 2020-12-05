using System;

namespace TilesDashboard.V2.Core.Entities.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
        }
    }
}
