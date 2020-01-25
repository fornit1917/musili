using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Musili.ApiApp.Models;
using Musili.ApiApp.Utils;

namespace Musili.ApiApp.Services.Db {
    public class TracksSourceRepository : ITracksSourcesRepository {
        private AppDbContext _db;

        public TracksSourceRepository(AppDbContext db) {
            _db = db;
        }

        public async Task<TracksSource> GetRandomTracksSource(TracksCriteria criteria) {
            var query = from ts in _db.TracksSources select ts;

            foreach (string[] tagsGroup in criteria.TagsGroups) {
                query = query.Where(ts => ts.Tags.Any(t => tagsGroup.Contains(t)));
            }

            int count = await query.CountAsync();
            int offset = RandomUtils.GetRandomFromInterval(0, count);
            query = query.OrderBy(ts => ts.Id).Skip(offset);

            return await query.FirstOrDefaultAsync();
        }


    }
}