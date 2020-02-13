using System;

namespace MetricsDashboard.Dto.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MetricKindTypeAttribute : Attribute
    {
        public MetricKindTypeAttribute(Type valueType)
        {
            ValueType = valueType;
        }

        public Type ValueType { get; }
    }
}