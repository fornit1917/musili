using System.Collections.Generic;
namespace Musili.WebApi.Models.Entities
{
    public class TracksSource
    {
        public int Id { get; set; }
        public TracksSourceService Service { get; set; }
        public TracksSourceType SourceType { get; set; }
        public string Value { get; set; }
        public string ParamsJson { get; set; }
        public ICollection<TracksSourceGenre> TracksSourceGenres { get; set; }
        public ICollection<TracksSourceTempo> TracksSourceTempos { get; set; }
    }
}