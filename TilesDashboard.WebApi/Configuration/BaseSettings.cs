using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace TilesDashboard.WebApi.Configuration
{
    public class BaseSettings
    {
        private readonly IConfiguration _configuration;

        public BaseSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected static T ConvertType<T, TD>(TD result)
        {
            return (T)Convert.ChangeType(result, typeof(T), CultureInfo.InvariantCulture);
        }

        protected T GetValue<T>(string key)
        {
            var value = GetValueOrDefault<T>(key);
            if (value == null)
            {
                throw new ArgumentException($"Missing configuration key - ${key}.");
            }

            return value;
        }

        protected T GetValueOrDefault<T>(string key)
        {
            var value = _configuration[key];
            if (value == null)
            {
                return default;
            }

            if (typeof(T) == typeof(string))
            {
                return ConvertType<T, string>(value);
            }

            if (typeof(T) == typeof(bool))
            {
                if (!bool.TryParse(value, out bool boolResult))
                {
                    ThrowArgumentException<T>();
                }

                return ConvertType<T, bool>(boolResult);
            }

            if (typeof(T) == typeof(int))
            {
                if (!int.TryParse(value, out int intResult))
                {
                    ThrowArgumentException<T>();
                }

                return ConvertType<T, int>(intResult);
            }

            if (typeof(T) == typeof(DateTime?))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return default;
                }

                return ConvertType<T, string>(value);
            }

            throw new InvalidOperationException("Not supported type.");
        }

        private static void ThrowArgumentException<T>()
        {
            throw new ArgumentException($"Invalid value for type {typeof(T)}.");
        }
    }
}
