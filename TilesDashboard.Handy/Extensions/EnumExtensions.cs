using System;

namespace TilesDashboard.Handy.Extensions
{
    public static class EnumExtensions
    {
        public static TDesc Convert<TDesc>(this Enum enumToConvert)
          where TDesc : Enum
        {
            return (TDesc)Enum.Parse(typeof(TDesc), enumToConvert.ToString(), true);
        }
    }
}
