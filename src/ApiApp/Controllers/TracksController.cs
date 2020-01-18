﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musili.ApiApp.Services;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Controllers {
    [Route("api/[controller]")]
    public class TracksController : Controller {
        private readonly ITracksProvider _tracksProvider;
        private readonly ITracksRequestsRating _tracksRequestsRating;

        public TracksController(ITracksProvider tracksProvider, ITracksRequestsRating tracksRequestsRating) {
            _tracksProvider = tracksProvider;
            _tracksRequestsRating = tracksRequestsRating;
        }

        [HttpGet("")]
        public async Task<List<Track>> GetTracks(string tempos, string genres, int lastId = 0) {
            TracksCriteria criteria = new TracksCriteria(tempos, genres);
            // _tracksRequestsRating.AddCriteria(criteria);
            return await _tracksProvider.GetTracks(criteria, lastId);
        }
    }
}