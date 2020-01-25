using Musili.ApiApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Musili.ApiApp.Services {
    public class BackgroundTracksLoadingList : IBackgroundTracksLoadingList {
        private ConcurrentDictionary<string, string> _criteriasByClients = new ConcurrentDictionary<string, string>();

        public void AddCriteria(string clientId, TracksCriteria criteria) {
            _criteriasByClients[clientId] = criteria.GetRandomMinimalCriteria().ToString();
        }

        public TracksCriteria[] GetDistinctCriteriasAndClear() {
            // todo: maybe it will be a good idea to use lock for atomically "GetAndRemove"
            TracksCriteria[] result = _criteriasByClients.Values.Distinct().Select(x => new TracksCriteria(x)).ToArray();
            _criteriasByClients.Clear();
            return result;
        }
    }
}
