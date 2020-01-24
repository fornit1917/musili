using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Musili.ApiApp.Models;
using System;
using System.Collections.Generic;
using Npgsql;

namespace Musili.ApiApp.Services.Db {
    public class TracksRepository : ITracksRepository {
        private AppDbContext _db;

        public TracksRepository(AppDbContext db) {
            _db = db;
        }

        public async Task<List<Track>> GetTracks(TracksCriteria tracksCriteria, int maxCount = 5, int lastId = 0) {
            var now = DateTime.Now;

            var query = _db.Tracks.Where(t => t.ExpirationDatetime > now);
            if (lastId > 0) {
                query = query.Where(t => t.Id > lastId).OrderBy(t => t.Id);
            } else {
                query = query.OrderByDescending(t => t.Id);
            }

            foreach (string[] tagsGroup in tracksCriteria.TagsGroups) {
                query = query.Where(t => t.TracksSource.Tags.Any(t => tagsGroup.Contains(t)));
            }

            query = query.Take(maxCount);
            return await query.ToListAsync();
        }

        public Task SaveTracks(List<Track> tracks) {
            _db.Tracks.AddRange(tracks);
            return _db.SaveChangesAsync();
        }

        public Task RemoveOldTracks(DateTime dateTime) {
            var commandText = "DELETE FROM app.track WHERE expiration_datetime <= @dt::timestamp";
            var param = new NpgsqlParameter("@dt", dateTime.ToString());
            return _db.Database.ExecuteSqlRawAsync(commandText, param);
        }
    }
}