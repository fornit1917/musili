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

        public async Task<List<Track>> GetTracksAsync(TracksCriteria criteria, int lastId = 0) {
            TracksSource tracksSource = await _tracksSourceRepository.GetRandomTracksSourceAsync(criteria);
            if (lastId == 0) {
                return await GrabAndSaveTracks(tracksSource);
            } else {
                List<Track> tracks = await _tracksRepository.GetTracksAsync(criteria, 5, lastId);
                if (tracks.Count == 0) {
                    return await GrabAndSaveTracks(tracksSource);
                }
                return tracks;
            }
        }

        private async Task<List<Track>> GrabAndSaveTracks(TracksSource tracksSource) {
            List<Track> tracks = await _grabber.GrabRandomTracksAsync(tracksSource);
            await _tracksRepository.SaveTracksAsync(tracks);
            return tracks;
        }
    }
}
