using Musili.ApiApp.Models;
using System;

namespace Musili.ApiApp.Services {
    public interface ITracksRequestsRating {
        void AddCriteria(TracksCriteria criteria, DateTime? dateTime = null);
        TracksCriteria[] GetHotCriterias(DateTime minDate);
        int RemoveOldRequests(DateTime minDate);
    }
}
