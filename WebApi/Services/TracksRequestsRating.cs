using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Musili.WebApi.Services {
    public class TracksRequestsRating : ITracksRequestsRating {
        private ConcurrentDictionary<string, DateTime> _items = new ConcurrentDictionary<string, DateTime>();

        public void AddCriteria(TracksCriteria criteria, DateTime? dateTime = null) {
            string key = GetCriteriaKey(criteria);
            DateTime dateTimeValue = dateTime ?? DateTime.Now;
            _items.AddOrUpdate(key, dateTimeValue, (existedKey, oldValue) => dateTimeValue);
        }

        public TracksCriteria[] GetHotCriterias(DateTime minDate) {
            return _items.Where(pair => pair.Value >= minDate).Select(pair => CreateCriteriaByKey(pair.Key)).ToArray();
        }

        public int RemoveOldRequests(DateTime minDate) {
            int count = 0;
            foreach (string key in _items.Keys) {
                if (_items[key] < minDate) {
                    if (_items.TryRemove(key, out DateTime removed)) {
                        count++;
                    }
                }
            }
            return count;
        }

        private static string GetCriteriaKey(TracksCriteria criteria) {
            return $"{string.Join(',', criteria.Tempos)};{string.Join(',', criteria.Genres)}";
        }

        private static TracksCriteria CreateCriteriaByKey(string key) {
            string[] parts = key.Split(";", 2);
            return new TracksCriteria(parts[0], parts[1]);
        }
    }
}
