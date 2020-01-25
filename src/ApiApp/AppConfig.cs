using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.ApiApp {
    public class AppConfig {
        public bool DeleteOldTracksInBackground { get; set; } = true;
        public bool LoadNewTracksInBackground { get; set; } = false;
        public int TracksUpdaterTimeout { get; set; } = 300;

        public int MaxConnectionsYandex { get; set; } = 3;
    }
}
