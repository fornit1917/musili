using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Utils;
using System;
using System.Collections.Generic;
using Npgsql;

namespace Musili.WebApi.Services.Db {
    public class TracksRepository : ITracksRepository {
        private AppDbContext _db;

        public TracksRepository(AppDbContext db) {
            _db = db;
        }

        public async Task<List<Track>> GetTracksAsync(TracksCriteria tracksCriteria, int maxCount = 5, int lastId = 0) {
            var now = DateTime.Now;

            var query = _db.Tracks
                .Where(t => t.Id > lastId && t.ExpirationDatetime > now);

            if (!tracksCriteria.IsAnyGenre) {
                query = query.Where(t => t.TracksSource.Genre == Genre.Any || tracksCriteria.Genres.Contains(t.TracksSource.Genre));
            }
            if (!tracksCriteria.IsAnyTempo) {
                query = query.Where(t => t.TracksSource.Tempo == Tempo.Any || tracksCriteria.Tempos.Contains(t.TracksSource.Tempo));
            }

            query = query.OrderBy(t => t.Id).Take(maxCount);

            return await query.ToListAsync();
        }

        public Task SaveTracksAsync(List<Track> tracks) {
            _db.Tracks.AddRange(tracks);
            return _db.SaveChangesAsync();
        }

        public Task RemoveOldTracksAsync(DateTime dateTime) {
            var commandText = "DELETE FROM app.track WHERE expiration_datetime <= @dt::timestamp";
            var param = new NpgsqlParameter("@dt", dateTime.ToString());
            return _db.Database.ExecuteSqlCommandAsync(commandText, param);
        }
    }
}