using System.Threading.Tasks;

namespace Musili.WebApi.Interfaces {
    public interface ITracksUpdater {
        Task RemoveOldTracksAsync();
        Task LoadNewTracksForAllCriteriasAsync();
    }
}