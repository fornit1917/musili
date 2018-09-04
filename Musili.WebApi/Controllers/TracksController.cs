﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musili.WebApi.Services.Db;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Models.Entities;

namespace Musili.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TracksController : Controller
    {
        private ITracksProvider tracksProvider;

        public TracksController(ITracksProvider tracksProvider, AppDbContext db) {
            this.tracksProvider = tracksProvider;
        }

        [HttpGet("")]
        public async Task<Track[]> GetTracks(string tempos, string genres, int lastId = 0) {
            TracksCriteria criteriaSet = new TracksCriteria(tempos, genres);
            return await tracksProvider.GetTracksAsync(criteriaSet, lastId);
        }

        [HttpGet("hello/{name}")]
        public string Hello(string name) {
            return $"Hello, {name}!";
        }
    }
}
