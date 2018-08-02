using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musili.WebApi.Db;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Models.Entities;
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

        [HttpGet("sources")]
        public async Task<List<TracksSource>> Sources(string tempo, string genres) {
            return await db.TracksSources.Where(t => t.Genre == Genre.Any || t.Genre == Genre.Rock).ToListAsync();
        }
    }
}
