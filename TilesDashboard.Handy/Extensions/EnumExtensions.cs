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

        public static bool Is<TEnum>(this TEnum enumToCheck, Enum toCompare)
          where TEnum : Enum
        {
            return Equals(enumToCheck.ToString(), toCompare.ToString());
        }

        public static bool IsNot<TEnum>(this TEnum enumToCheck, Enum toCompare)
            where TEnum : Enum
        {
            return !enumToCheck.Is(toCompare);
        }
    }
}
