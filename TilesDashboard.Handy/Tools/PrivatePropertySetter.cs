using System.Reflection;

namespace TilesDashboard.Handy.Tools
{
    public static class PrivatePropertySetter
    {
        public static void SetProperty<T>(T objectWithPropertyToSet, string propertyName, object valueToSet)
        {
            var prop = typeof(T).GetProperty(propertyName);
            prop.SetValue(objectWithPropertyToSet, valueToSet);
        }

        public static void SetPropertyWithNoSetter<T>(T objectWithPropertyToSet, string propertyName, object valueToSet)
        {
            var prop = typeof(T).GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(objectWithPropertyToSet, valueToSet);
        }
    }
}
