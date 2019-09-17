using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services {
    public class TracksProvider : ITracksProvider {
        private ITracksSourcesRepository _tracksSourceRepository;
        private ITracksRepository _tracksRepository;
        private ICommonTracksGrabber _grabber;

        public TracksProvider(ITracksSourcesRepository tracksSourceRepository, ITracksRepository tracksRepository, ICommonTracksGrabber grabber) {
            _tracksSourceRepository = tracksSourceRepository;
            _tracksRepository = tracksRepository;
            _grabber = grabber;
        }

        public async Task<List<Track>> GetTracks(TracksCriteria criteria, int lastId = 0) {
            List<Track> tracks = await _tracksRepository.GetTracks(criteria, 5, lastId);
            if (tracks.Count == 0) {
                return await GrabAndSaveTracks(criteria);
            }
            return tracks;
        }

        public async Task<List<Track>> GrabAndSaveTracks(TracksCriteria criteria) {
            TracksSource tracksSource = await _tracksSourceRepository.GetRandomTracksSource(criteria);
            List<Track> tracks = await _grabber.GrabRandomTracks(tracksSource);
            await _tracksRepository.SaveTracks(tracks);
            return tracks;
        }
    }
}
