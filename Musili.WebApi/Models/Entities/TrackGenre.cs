using Musili.WebApi.Models;

namespace Musili.WebApi.Models.Entities
{
    public class TrackGenre
    {
        public int Id { get; set; }
        public Genre Genre { get; set; }
    }
}