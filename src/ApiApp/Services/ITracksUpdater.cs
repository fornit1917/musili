using System.Threading.Tasks;

namespace Musili.ApiApp.Services {
    public interface ITracksUpdater {
        Task RemoveOldTracks();
        Task LoadNewTracksForHotCriterias();
    }
}