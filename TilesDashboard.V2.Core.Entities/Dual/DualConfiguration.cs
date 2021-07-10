using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.V2.Core.Entities.Dual
{
    public class DualConfiguration
    {
        public DualConfiguration(IDictionary<string, object> configurationAsDictionary)
        {
            if (configurationAsDictionary.NotExists())
            {
                return;
            }

            PrimaryName = configurationAsDictionary.GetValue<string>(nameof(PrimaryName));
            PrimaryUnit = configurationAsDictionary.GetValueOrDefault<string>(nameof(PrimaryUnit));
            PrimaryIntegerOnly = configurationAsDictionary.GetValueOrDefault<bool>(nameof(PrimaryIntegerOnly));
            PrimaryMaxGraphValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(PrimaryMaxGraphValue));
            PrimaryMinGraphValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(PrimaryMinGraphValue));
            PrimaryGraphColor = configurationAsDictionary.GetValueOrDefault<string>(nameof(PrimaryGraphColor));

            PrimaryGreenValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(PrimaryGreenValue));
            PrimaryYellowValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(PrimaryYellowValue));
            PrimaryRedValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(PrimaryRedValue));

            SecondaryName = configurationAsDictionary.GetValue<string>(nameof(SecondaryName));
            SecondaryUnit = configurationAsDictionary.GetValueOrDefault<string>(nameof(SecondaryUnit));
            SecondaryIntegerOnly = configurationAsDictionary.GetValueOrDefault<bool>(nameof(SecondaryIntegerOnly));
            SecondaryMaxGraphValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(SecondaryMaxGraphValue));
            SecondaryMinGraphValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(SecondaryMinGraphValue));
            SecondaryGraphColor = configurationAsDictionary.GetValueOrDefault<string>(nameof(SecondaryGraphColor));

            SecondaryGreenValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(SecondaryGreenValue));
            SecondaryYellowValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(SecondaryYellowValue));
            SecondaryRedValue = configurationAsDictionary.GetValueOrNull<decimal>(nameof(SecondaryRedValue));

            LowerIsBetter = configurationAsDictionary.GetValueOrDefault<bool>(nameof(LowerIsBetter));
            PrimaryAndSecondaryHaveTheSameYAxis = configurationAsDictionary.GetValueOrDefault<bool>(nameof(PrimaryAndSecondaryHaveTheSameYAxis));
        }

        public string PrimaryName { get; private set; }

        public string PrimaryUnit { get; private set; }

        public bool PrimaryIntegerOnly { get; private set; }

        public decimal? PrimaryMaxGraphValue { get; private set; }

        public decimal? PrimaryMinGraphValue { get; private set; }

        public decimal? PrimaryGreenValue { get; private set; }

        public decimal? PrimaryYellowValue { get; private set; }

        public decimal? PrimaryRedValue { get; private set; }

        public string PrimaryGraphColor { get; private set; }

        public string SecondaryName { get; private set; }

        public string SecondaryUnit { get; private set; }

        public bool SecondaryIntegerOnly { get; private set; }

        public decimal? SecondaryMaxGraphValue { get; private set; }

        public decimal? SecondaryMinGraphValue { get; private set; }

        public string SecondaryGraphColor { get; private set; }

        public decimal? SecondaryGreenValue { get; private set; }

        public decimal? SecondaryYellowValue { get; private set; }

        public decimal? SecondaryRedValue { get; private set; }

        public bool PrimaryAndSecondaryHaveTheSameYAxis { get; private set; }

        public bool LowerIsBetter { get; private set; }
    }
}
