using Microsoft.EntityFrameworkCore;
using Musili.WebApi.Models.Entities;

namespace Musili.WebApi.Services.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<TracksSource> TracksSources { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {
        }
    }    
}