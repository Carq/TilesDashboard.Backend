using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.V2.Core.Entities.Integer
{
    public class IntegerConfiguration
    {
        public IntegerConfiguration(IDictionary<string, object> configurationAsDictionary)
        {
            Unit = configurationAsDictionary.GetValueOrDefault<string>(nameof(Unit));
            Description = configurationAsDictionary.GetValueOrDefault<string>(nameof(Description));
        }

        public string Unit { get; private set; }

        public string Description { get; private set; }
    }
}
