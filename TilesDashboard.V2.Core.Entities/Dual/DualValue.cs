using System;

namespace TilesDashboard.V2.Core.Entities.Dual
{
    public class DualValue : TileValue
    {
        public DualValue(decimal primary, decimal secondary, DateTimeOffset addedOn)
            : base(addedOn)
        {
            Primary = ParseAndValidate(primary);
            Secondary = ParseAndValidate(secondary);
        }

        public decimal Primary { get; private set; }

        public decimal Secondary { get; private set; }

        protected static decimal ParseAndValidate(decimal value)
        {
            return Math.Round(value, 1);
        }
    }
}
