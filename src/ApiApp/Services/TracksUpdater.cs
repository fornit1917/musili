using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NLog;
using Musili.ApiApp.Models;
using Musili.ApiApp.Services.Db;

namespace Musili.ApiApp.Services {
    public class TracksUpdater : ITracksUpdater {
        private readonly ITracksRepository _tracksRepository;
        private readonly ITracksProvider _tracksProvider;
        private readonly ILogger<TracksUpdater> _logger;

        public TracksUpdater(ITracksRepository tracksRepository, ITracksProvider tracksProvider, ILogger<TracksUpdater> logger) {
            _tracksRepository = tracksRepository;
            _tracksProvider = tracksProvider;
            _logger = logger;
        }

        public async Task LoadNewTracksForHotCriterias(int hotCriteriaLifeTime) {
            using (MappedDiagnosticsLogicalContext.SetScoped("jobId", "load-tracks")) {

                TracksCriteria[] hotCriterias = Array.Empty<TracksCriteria>(); // todo: implement it

                _logger.LogTrace("Count of hot criterias for background tracks loading: {0}", hotCriterias.Length);
                foreach (var criteria in hotCriterias) {
                    try {
                        List<Track> tracks = await _tracksProvider.GrabAndSaveTracks(criteria);
                        _logger.LogInformation("Loaded {0} tracks in background. Criteria: {1}", tracks.Count, criteria.ToString());
                    } catch (Exception ex) {
                        _logger.LogError(ex, "Failed to load tracks in background. Criteria: {0}", criteria.ToString());
                    }
                }
            }
        }

        public Task RemoveOldTracks() {
            using (MappedDiagnosticsLogicalContext.SetScoped("jobId", "remove-old-tracks")) {
                return _tracksRepository.RemoveOldTracks(DateTime.Now);
            }
        }
    }
}