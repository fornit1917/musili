using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services {
    public class TracksUpdater : ITracksUpdater {
        private ITracksRepository _tracksRepository;
        private ITracksSourcesRepository _tracksSourcesRepository;
        private ICommonTracksGrabber _tracksGrabber;

        public TracksUpdater(ITracksRepository tracksRepository, ITracksSourcesRepository tracksSourcesRepository, ICommonTracksGrabber tracksGrabber) {
            _tracksRepository = tracksRepository;
            _tracksSourcesRepository = tracksSourcesRepository;
            _tracksGrabber = tracksGrabber;
        }

        public async Task LoadNewTracksForAllCriteriasAsync(int maxTime) {
            foreach (string genre in Enum.GetNames(typeof(Genre))) {
                foreach (string tempo in Enum.GetNames(typeof(Tempo))) {
                    if (genre == "Any" || tempo == "Any") {
                        continue;
                    }
                    int time = 0;
                    int requestsCount = 0;
                    while (requestsCount < 10 && time < maxTime) {
                        try {
                            var criteria = new TracksCriteria(tempo, genre);
                            TracksSource tracksSource = await _tracksSourcesRepository.GetRandomTracksSourceAsync(criteria);
                            List<Track> tracks = await _tracksGrabber.GrabRandomTracksAsync(tracksSource);
                            int duration = tracks.Select(t => t.Duration).Sum();
                            time += duration;
                            await _tracksRepository.SaveTracksAsync(tracks);
                            //Console.WriteLine($"{genre} - {tempo}: {tracks.Count} tracks");
                            await Task.Delay(1000);
                        } catch {
                            // todo: log
                        }
                        requestsCount++;
                    }
                }
            }
        }

        public Task RemoveOldTracksAsync() {
            return _tracksRepository.RemoveOldTracksAsync(DateTime.Now);
        }
    }
}