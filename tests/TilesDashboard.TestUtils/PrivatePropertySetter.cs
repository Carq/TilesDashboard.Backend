using System.Reflection;

namespace TilesDashboard.TestUtils
{
    public static class PrivatePropertySetter
    {
        public static void SetProperty<T>(T objectWithPropertyToSet, string propertyName, object valueToSet)
        {
            var prop = typeof(T).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
            prop?.SetValue(objectWithPropertyToSet, valueToSet);
        }

        public static void SetPropertyWithNoSetter<T>(T objectWithPropertyToSet, string propertyName, object valueToSet)
        {
            var prop = typeof(T).GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            if (prop == null && typeof(T).BaseType != null)
            {
                prop = typeof(T).BaseType.GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            prop?.SetValue(objectWithPropertyToSet, valueToSet);
        }
    }
}
