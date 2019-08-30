using System.Threading.Tasks;

namespace Musili.WebApi.Interfaces {
    public interface ITracksUpdater {
        Task RemoveOldTracks();
        Task LoadNewTracksForHotCriterias(int hotCriteriaLifeTime);
    }
}