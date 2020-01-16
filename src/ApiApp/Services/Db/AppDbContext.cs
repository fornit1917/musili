using Microsoft.EntityFrameworkCore;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Services.Db {
    public class AppDbContext : DbContext {
        public DbSet<TracksSource> TracksSources { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
    }
}