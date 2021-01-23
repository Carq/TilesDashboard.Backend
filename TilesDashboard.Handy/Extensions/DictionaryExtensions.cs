using System;
using System.Collections.Generic;
using System.Globalization;

namespace TilesDashboard.Handy.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetValue<T>(this IDictionary<string, object> source, string key)
        {
            var value = source.GetValueOrDefault<T>(key);
            if (value == null)
            {
                throw new ArgumentException($"Missing configuration key - ${key}.");
            }

            return value;
        }

        public static T GetValueOrDefault<T>(this IDictionary<string, object> source, string key)
        {
            var keyExists = source.TryGetValue(key, out object objectValue);
            if (!keyExists)
            {
                return default;
            }

            return ConvertType<T>(objectValue);
        }

        public static T? GetValueOrNull<T>(this IDictionary<string, object> source, string key)
            where T : struct
        {
            var keyExists = source.TryGetValue(key, out object objectValue);
            if (!keyExists)
            {
                return default(T?);
            }

            return ConvertType<T>(objectValue);
        }

        private static T ConvertType<T>(object objectValue)
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), objectValue.ToString());
            }

            return (T)Convert.ChangeType(objectValue, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}
