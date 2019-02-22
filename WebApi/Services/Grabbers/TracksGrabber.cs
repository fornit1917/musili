using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers {
    public class TracksGrabber : ICommonTracksGrabber {
        private Func<TracksSourceService, ITracksGrabber> _grabbersProvider;

        public TracksGrabber(Func<TracksSourceService, ITracksGrabber> grabbersProvider) {
            _grabbersProvider = grabbersProvider;
        }

        public async Task<List<Track>> GrabRandomTracksAsync(TracksSource tracksSource) {
            ITracksGrabber grabber = _grabbersProvider(tracksSource.Service);
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