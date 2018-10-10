using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Utils;
using System;
using System.Collections.Generic;

namespace Musili.WebApi.Services.Db
{
    public class TracksRepository : ITracksRepository
    {
        private AppDbContext db;

        public TracksRepository(AppDbContext db) {
            this.db = db;
        }

        public async Task<List<Track>> GetTracksAsync(TracksCriteria tracksCriteria, int maxCount = 5, int lastId = 0)
        {
            var query = db.Tracks
                .Join(db.TracksSources, t => t.TracksSourceId, ts => ts.Id, (t, ts) => t)
                .Where(t => t.Id > lastId)
                .OrderBy(t => t.Id)
                .Take(maxCount);
                
            if (!tracksCriteria.IsAnyGenre) {
                query = query.Where(t => t.TracksSource.Genre == Genre.Any || tracksCriteria.Genres.Contains(t.TracksSource.Genre));
            }
            if (!tracksCriteria.IsAnyTempo) {
                query = query.Where(t => t.TracksSource.Tempo == Tempo.Any || tracksCriteria.Tempos.Contains(t.TracksSource.Tempo));
            }

            Console.WriteLine(query.ToSql());
            
            return await query.ToListAsync();
        }

        public Task SaveTracksAsync(List<Track> tracks)
        {
            db.Tracks.AddRange(tracks);
            return db.SaveChangesAsync();
        }
    }
}