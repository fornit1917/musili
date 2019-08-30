using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces {
    public interface ITracksGrabber {
        TimeSpan LinkLifeTime { get; }

        Task<List<Track>> GrabRandomTracks(TracksSource tracksSource);
    }
}