using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using System;
using System.Collections.Generic;
using Npgsql;

namespace Musili.WebApi.Services.Db {
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

            if (!tracksCriteria.IsAnyGenre) {
                query = query.Where(t => t.TracksSource.Genre == Genre.Any || tracksCriteria.Genres.Contains(t.TracksSource.Genre));
            }
            if (!tracksCriteria.IsAnyTempo) {
                query = query.Where(t => t.TracksSource.Tempo == Tempo.Any || tracksCriteria.Tempos.Contains(t.TracksSource.Tempo));
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