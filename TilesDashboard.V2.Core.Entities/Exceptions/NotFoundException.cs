using System;

namespace TilesDashboard.V2.Core.Entities.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
