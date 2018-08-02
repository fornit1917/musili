using Musili.WebApi.Models;

namespace Musili.WebApi.Models.Entities
{
    public class TracksSourceTempo
    {
        public int TracksSourceId { get; set; }
        public Tempo Tempos { get; set; }
    }
}