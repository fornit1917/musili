using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musili.WebApi.Services.Db;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace Musili.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private ITracksProvider tracksProvider;
        private AppDbContext db;

        public TestController(AppDbContext db, ITracksProvider tracksProvider) {
            this.db = db;
            this.tracksProvider = tracksProvider;
        }

        [HttpGet("source")]
        public async Task<TracksSource> Source(string tempo, string genres, [FromServices] ITracksSourcesRepository sourcesRepository) {
            return await sourcesRepository.GetRandomTracksSourceAsync(new TracksCriteria(tempo, genres));
        }

        [HttpGet("")]
        public object Index() {
            return new Object();
        }
    }
}
