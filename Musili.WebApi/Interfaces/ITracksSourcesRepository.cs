using System.Threading.Tasks;
using Musili.WebApi.Models;
using Musili.WebApi.Models.Entities;

namespace Musili.WebApi.Interfaces
{
    public interface ITracksSourcesRepository
    {
         Task<TracksSource> GetRandomTracksSourceAsync(TracksCriteria criteria);
    }
}