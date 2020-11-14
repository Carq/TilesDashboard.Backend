using System;

namespace TilesDashboard.V2.Core.Entities.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message)
            : base(message)
        {
        }
    }
}
