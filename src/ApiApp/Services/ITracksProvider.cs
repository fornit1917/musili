using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Services {
    public interface ITracksProvider {
        Task<List<Track>> GetTracks(TracksCriteria criteria, int lastId = 0);
        Task<List<Track>> GrabAndSaveTracks(TracksCriteria criteria);
    }
}
