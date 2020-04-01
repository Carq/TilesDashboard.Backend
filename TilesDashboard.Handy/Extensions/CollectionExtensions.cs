using System;
using System.Collections.Generic;
using System.Linq;

namespace TilesDashboard.Handy.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> source, ICollection<T> collection)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (collection == null)
            {
                return;
            }

            if (source is List<T> list)
            {
                list.AddRange(collection);
                return;
            }

            foreach (var i in collection)
            {
                source.Add(i);
            }
        }

        public static bool IsEmpty<T>(this ICollection<T> list)
        {
            return !list.Any();
        }

        public static bool IsNotEmpty<T>(this ICollection<T> list)
        {
            return list.Any();
        }

        public static bool IsEmpty<T>(this T[] array)
        {
            return !array.Any();
        }

        public static bool IsNotEmpty<T>(this T[] array)
        {
            return array.Any();
        }
    }
}
