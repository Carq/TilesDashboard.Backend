using System;
using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Core.Exceptions
{
    public class InvalidTileTypeException : Exception
    {
        public InvalidTileTypeException(string name, TileTypeDto actual, TileTypeDto expected)
            : base($"Invalid {name} tile type, it is of type {actual} but expecting {expected}.")
        {
        }
    }
}