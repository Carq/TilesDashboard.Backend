using System;

namespace TilesDashboard.Core.Exceptions
{
    public class NotSupportedOperationException : Exception
    {
        public NotSupportedOperationException(string message)
            : base(message)
        {
        }
    }
}
