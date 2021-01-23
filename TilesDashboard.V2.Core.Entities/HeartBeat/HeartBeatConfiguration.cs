using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.V2.Core.Entities.HeartBeat
{
    public class HeartBeatConfiguration
    {
        public HeartBeatConfiguration(IDictionary<string, object> configurationAsDictionary)
        {
            ApplicationUrl = configurationAsDictionary.GetValue<string>(nameof(ApplicationUrl));
            ApplicationHeartBeatUrl = configurationAsDictionary.GetValue<string>(nameof(ApplicationHeartBeatUrl));
            Description = configurationAsDictionary.GetValueOrDefault<string>(nameof(Description));
        }

        public string ApplicationUrl { get; private set; }

        public string ApplicationHeartBeatUrl { get; private set; }

        public string Description { get; private set; }
    }
}
