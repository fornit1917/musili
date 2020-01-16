using System.Threading.Tasks;

namespace Musili.ApiApp.Interfaces {
    public interface ITracksUpdater {
        Task RemoveOldTracks();
        Task LoadNewTracksForHotCriterias(int hotCriteriaLifeTime);
    }
}