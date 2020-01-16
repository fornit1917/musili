using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Services.Grabbers {
    public interface IServiceTracksGrabber {
        TimeSpan LinkLifeTime { get; }

        Task<List<Track>> GrabRandomTracks(TracksSource tracksSource);
    }
}