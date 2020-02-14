using System;

namespace MetricsDashboard.Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MetricKindTypeAttribute : Attribute
    {
        public MetricKindTypeAttribute(Type valueType)
        {
            ValueType = valueType;
        }

        public Type ValueType { get; }
    }
}