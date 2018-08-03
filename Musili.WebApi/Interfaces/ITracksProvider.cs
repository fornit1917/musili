using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musili.WebApi.Models;
using Musili.WebApi.Models.Entities;

namespace Musili.WebApi.Interfaces
{
    public interface ITracksProvider
    {
        Task<List<Track>> GetTracksAsync(TracksCriteriaSet criteria, int lastId = 0);
    }
}
