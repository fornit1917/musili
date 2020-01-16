using System.Threading.Tasks;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Services.Db {
    public interface ITracksSourcesRepository {
        Task<TracksSource> GetRandomTracksSource(TracksCriteria criteria);
    }
}