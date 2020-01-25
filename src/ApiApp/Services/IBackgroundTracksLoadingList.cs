using Musili.ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.ApiApp.Services {
    public interface IBackgroundTracksLoadingList {
        void AddCriteria(string clientId, TracksCriteria criteria);
        TracksCriteria[] GetDistinctCriteriasAndClear();
    }
}
