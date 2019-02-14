using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services
{
    public class TracksUpdater : ITracksUpdater
    {
        private ITracksRepository tracksRepository;
        private ITracksSourcesRepository tracksSourcesRepository;
        private ICommonTracksGrabber tracksGrabber;

        public TracksUpdater(ITracksRepository tracksRepository, ITracksSourcesRepository tracksSourcesRepository, ICommonTracksGrabber tracksGrabber) {
            this.tracksRepository = tracksRepository;
            this.tracksSourcesRepository = tracksSourcesRepository;
            this.tracksGrabber = tracksGrabber;
        }

        public async Task LoadNewTracksForAllCriteriasAsync() {
            int maxTime = 60*30;
            foreach (string genre in Enum.GetNames(typeof(Genre))) {
                foreach(string tempo in Enum.GetNames(typeof(Tempo))) {
                    if (genre == "Any" || tempo == "Any") {
                        continue;
                    }
                    int time = 0;
                    int requestsCount = 0;
                    while (requestsCount < 10 && time < maxTime) {
                        try {
                            var criteria = new TracksCriteria(tempo, genre);
                            TracksSource tracksSource = await tracksSourcesRepository.GetRandomTracksSourceAsync(criteria);
                            List<Track> tracks = await tracksGrabber.GrabRandomTracksAsync(tracksSource);
                            int duration = tracks.Select(t => t.Duration).Sum();
                            time += duration;
                            await tracksRepository.SaveTracksAsync(tracks);
                            //Console.WriteLine($"{genre} - {tempo}: {tracks.Count} tracks");
                            await Task.Delay(1000);
                        } catch (Exception e) {
                            // todo: log
                        }
                        requestsCount++;
                    }
                }
            }
        }

        public Task RemoveOldTracksAsync() {
            return tracksRepository.RemoveOldTracksAsync(DateTime.Now);
        }
    }
}