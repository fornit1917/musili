using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Models.Entities;

namespace Musili.WebApi.Services
{
    public class TracksProvider : ITracksProvider
    {
        private ITracksSourcesRepository tracksSourceRepository;

        public TracksProvider(ITracksSourcesRepository tracksSourceRepository) {
            this.tracksSourceRepository = tracksSourceRepository;
        }

        public Task<Track[]> GetTracksAsync(TracksCriteria criteria, int lastId = 0) {
            var result = new Track[] {
                new Track(){ Artist = criteria.Genres.ToString(), Title = "Title 1", Url = "Url 1" },
                new Track(){ Artist = criteria.Tempos.ToString(), Title = "Title 2", Url = "Url 2" },
            };
            return Task.FromResult(result);
        }
    }
}
