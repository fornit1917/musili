using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApi.Models.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int TracksSourceId { get; set; }

        public ICollection<TrackTempo> TrackTempos { get; set; }
        public ICollection<TrackGenre> TrackGenres { get; set; }
        public TracksSource TracksSource { get; set; }
    }
}
