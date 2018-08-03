using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Models.Entities;
using Musili.WebApi.Utils;
using System;

namespace Musili.WebApi.Services.Db
{
    public class TracksSourceRepository : ITracksSourcesRepository
    {
        private AppDbContext db;

        public TracksSourceRepository(AppDbContext db) {
            this.db = db;
        }

        public async Task<TracksSource> GetRandomTracksSourceAsync(TracksCriteria criteria) {
            var query = from ts in db.TracksSources select ts;
            if (!criteria.IsAnyTempo) {
                query = query.Where(ts => ts.Tempo == Tempo.Any || criteria.Tempos.Contains(ts.Tempo));
            }
            if (!criteria.IsAnyGenre) {
                query = query.Where(ts => ts.Genre == Genre.Any || criteria.Genres.Contains(ts.Genre));
            }

            int count = await query.CountAsync();
            int offset = RandomUtils.GetRandomFromInterval(0, count);
            query = query.Skip(offset).Take(1);
            
            return await query.FirstOrDefaultAsync();
        }
    }
}