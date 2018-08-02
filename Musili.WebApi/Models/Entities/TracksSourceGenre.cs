
using Musili.WebApi.Models;

namespace Musili.WebApi.Models.Entities
{
    public class TracksSourceGenre
    {
        public int TracksSourceId { get; set; }
        public Genre Genre { get; set; }
    }
}