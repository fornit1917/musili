using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApi {
    public class AppConfig {
        public bool DeleteOldTracksInBackground { get; set; } = true;
        public bool LoadNewTracksInBackground { get; set; } = false;
        public int TracksUpdaterTimeoutSeconds { get; set; } = 3600;
        public int TracksUpdaterMaxDurationSeconds { get; set; } = 1800;

        public int MaxConnectionsYandex { get; set; } = 3;
    }
}
