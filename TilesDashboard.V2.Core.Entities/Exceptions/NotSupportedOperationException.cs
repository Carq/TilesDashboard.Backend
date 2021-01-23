using System;

namespace TilesDashboard.V2.Core.Entities.Exceptions
{
    public class NotSupportedOperationException : Exception
    {
        public NotSupportedOperationException(string message)
            : base(message)
        {
        }
    }
}
