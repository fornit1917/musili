using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces {
    public interface ITracksSourcesRepository {
        Task<TracksSource> GetRandomTracksSource(TracksCriteria criteria);
    }
}