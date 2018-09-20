using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services
{
    public class TracksProvider : ITracksProvider
    {
        private ITracksSourcesRepository tracksSourceRepository;
        private ICommonTracksGrabber grabber;

        public TracksProvider(ITracksSourcesRepository tracksSourceRepository, ICommonTracksGrabber grabber) {
            this.tracksSourceRepository = tracksSourceRepository;
            this.grabber = grabber;
        }

        public async Task<List<Track>> GetTracksAsync(TracksCriteria criteria, int lastId = 0) {
            TracksSource tracksSource =  await tracksSourceRepository.GetRandomTracksSourceAsync(criteria);
            return await grabber.GrabRandomTracksAsync(tracksSource);
        }
    }
}
