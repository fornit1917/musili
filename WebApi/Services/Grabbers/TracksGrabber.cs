using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers
{
    public class TracksGrabber : ICommonTracksGrabber
    {
        private Func<TracksSourceService, ITracksGrabber> grabbersProvider;

        public TracksGrabber(Func<TracksSourceService, ITracksGrabber> grabbersProvider) {
            this.grabbersProvider = grabbersProvider;
        }

        public async Task<List<Track>> GrabRandomTracksAsync(TracksSource tracksSource) {
            ITracksGrabber grabber = grabbersProvider(tracksSource.Service);
            DateTime expirationDatetime = DateTime.Now.Add(grabber.LinkLifeTime);
            List<Track> tracks = await grabber.GrabRandomTracksAsync(tracksSource);
            foreach (var track in tracks) {
                track.TracksSource = tracksSource;
                track.ExpirationDatetime = expirationDatetime;
            }
            return tracks;
        }
    }
}