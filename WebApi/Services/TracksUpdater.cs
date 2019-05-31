using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Microsoft.Extensions.Logging;

namespace Musili.WebApi.Services {
    public class TracksUpdater : ITracksUpdater {
        private readonly ITracksRepository _tracksRepository;
        private readonly ITracksRequestsRating _tracksRequestsRating;
        private readonly ITracksProvider _tracksProvider;
        private readonly ILogger<TracksUpdater> _logger;

        public TracksUpdater(ITracksRepository tracksRepository, ITracksRequestsRating tracksRequestsRating, ITracksProvider tracksProvider, ILogger<TracksUpdater> logger) {
            _tracksRepository = tracksRepository;
            _tracksRequestsRating = tracksRequestsRating;
            _tracksProvider = tracksProvider;
            _logger = logger;
        }

        public async Task LoadNewTracksForHotCriteriasAsync(int hotCriteriaLifeTime) {
            DateTime minRequestDatetime = DateTime.Now.Subtract(TimeSpan.FromSeconds(hotCriteriaLifeTime));
            _tracksRequestsRating.RemoveOldRequests(minRequestDatetime);
            TracksCriteria[] hotCriterias = _tracksRequestsRating.GetHotCriterias(minRequestDatetime);
            _logger.LogTrace("Count of hot criterias for background tracks loading: {0}", hotCriterias.Length);
            foreach(var criteria in hotCriterias) {
                try {
                    List<Track> tracks = await _tracksProvider.GrabAndSaveTracks(criteria);
                    _logger.LogInformation("Loaded in background {0} tracks by criteria {1}", tracks.Count, criteria.ToString());
                } catch (Exception ex) {
                    _logger.LogError(ex, "Failed to background loading tracks by criteria {0}", criteria.ToString());
                }
            }
        }

        public Task RemoveOldTracksAsync() {
            return _tracksRepository.RemoveOldTracksAsync(DateTime.Now);
        }
    }
}