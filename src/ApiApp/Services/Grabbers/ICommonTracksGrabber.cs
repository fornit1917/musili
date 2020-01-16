using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Services.Grabbers {
    public interface ICommonTracksGrabber {
        Task<List<Track>> GrabRandomTracks(TracksSource tracksSource);
    }
}