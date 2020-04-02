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

        public static bool Is<TEnum>(this TEnum enumToConvert, Enum toCompare)
          where TEnum : Enum
        {
            return Equals(enumToConvert.ToString(), toCompare.ToString());
        }

        public static bool IsNot<TEnum>(this TEnum enumToConvert, Enum toCompare)
            where TEnum : Enum
        {
            return !enumToConvert.Is(toCompare);
        }
    }
}
