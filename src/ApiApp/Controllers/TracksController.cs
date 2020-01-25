using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musili.ApiApp.Services;
using Musili.ApiApp.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace Musili.ApiApp.Controllers {
    [Route("api/[controller]")]
    public class TracksController : Controller {
        private readonly ITracksProvider _tracksProvider;
        private readonly IBackgroundTracksLoadingList _backgroundTracksLoadingList;

        public TracksController(ITracksProvider tracksProvider, IBackgroundTracksLoadingList backgroundTracksLoadingList) {
            _tracksProvider = tracksProvider;
            _backgroundTracksLoadingList = backgroundTracksLoadingList;
        }

        [HttpGet("")]
        public async Task<List<Track>> GetTracks(string tempos, string genres, int lastId = 0) {
            TracksCriteria criteria = new TracksCriteria(tempos, genres);

            // todo: use something like middleware for it
            string clientId = Request.Cookies["clientId"];
            if (clientId == null) {
                clientId = Guid.NewGuid().ToString();
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddYears(100);
                Response.Cookies.Append("clientId", clientId, options);
            }

            _backgroundTracksLoadingList.AddCriteria(clientId, criteria);

            return await _tracksProvider.GetTracks(criteria, lastId);
        }        
    }
}
