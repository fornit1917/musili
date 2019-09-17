using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces {
    public interface ITracksRepository {
        Task<List<Track>> GetTracks(TracksCriteria tracksCriteria, int maxCount = 5, int lastId = 0);
        Task SaveTracks(List<Track> tracks);
        Task RemoveOldTracks(DateTime dateTime);
    }
}