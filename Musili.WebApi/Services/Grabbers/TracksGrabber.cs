using System;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers
{
    public class TracksGrabber : ITracksGrabber
    {
        private Func<TracksSourceService, ITracksGrabber> grabbersProvider;

        public TracksGrabber(Func<TracksSourceService, ITracksGrabber> grabbersProvider) {
            this.grabbersProvider = grabbersProvider;
        }

        public Task<Track[]> GrabRandomTracksAsync(TracksSource tracksSource) {
            ITracksGrabber grabber = grabbersProvider(tracksSource.Service);
            return grabber.GrabRandomTracksAsync(tracksSource);
        }
    }
}