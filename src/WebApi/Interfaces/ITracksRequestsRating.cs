using Musili.WebApi.Models;
using System;

namespace Musili.WebApi.Interfaces {
    public interface ITracksRequestsRating {
        void AddCriteria(TracksCriteria criteria, DateTime? dateTime = null);
        TracksCriteria[] GetHotCriterias(DateTime minDate);
        int RemoveOldRequests(DateTime minDate);
    }
}
