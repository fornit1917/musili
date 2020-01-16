using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musili.ApiApp.Services.Db;
using Musili.ApiApp.Interfaces;
using Musili.ApiApp.Models;
using Microsoft.Extensions.Logging;

namespace Musili.ApiApp.Controllers {
    [Route("api/[controller]")]
    public class TestController : Controller {
        private ITracksProvider _tracksProvider;
        private AppDbContext _db;
        private readonly ILogger<TestController> _logger;

        public TestController(AppDbContext db, ITracksProvider tracksProvider, ILogger<TestController> logger) {
            _db = db;
            _tracksProvider = tracksProvider;
            _logger = logger;
        }

        [HttpGet("source")]
        public async Task<TracksSource> Source(string tempo, string genres, [FromServices] ITracksSourcesRepository sourcesRepository) {
            return await sourcesRepository.GetRandomTracksSource(new TracksCriteria(tempo, genres));
        }

        [HttpGet("")]
        public object Index() {
            return new Object();
        }

        [HttpGet("logs")]
        public string Logs() {
            _logger.LogInformation("Test info message");
            _logger.LogError("Test error message");
            return "Ok";
        }

        [HttpGet("error")]
        public string Error() {
            throw new Exception("Error message");
        }
    }
}
