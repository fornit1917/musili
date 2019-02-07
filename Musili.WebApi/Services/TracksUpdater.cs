using System;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;

namespace Musili.WebApi.Services
{
    public class TracksUpdater : ITracksUpdater
    {
        private ITracksRepository tracksRepository;

        public TracksUpdater(ITracksRepository tracksRepository) {
            this.tracksRepository = tracksRepository;
        }

        public Task LoadNewTracksForAllCriteriasAsync() {
            // todo: implement it
            return Task.CompletedTask;
        }

        public Task RemoveOldTracksAsync() {
            return tracksRepository.RemoveOldTracksAsync(DateTime.Now);
        }
    }
}