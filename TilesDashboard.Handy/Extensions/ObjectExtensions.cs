using System.Collections.Generic;

namespace TilesDashboard.Handy.Extensions
{
    public static class ObjectExtensions
    {
        public static bool Exists(this object singleObject)
        {
            return singleObject != null;
        }

        public static bool NotExists(this object singleObject)
        {
            return !singleObject.Exists();
        }

        public static IList<T> ToOneElementList<T>(this T element)
        {
            return new List<T> { element };
        }
    }
}
