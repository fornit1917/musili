using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces {
    public interface ITracksProvider {
        Task<List<Track>> GetTracksAsync(TracksCriteria criteria, int lastId = 0);
    }
}
