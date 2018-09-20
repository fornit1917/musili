using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Models
{
    [Table("track", Schema="app")]
    public class Track
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("artist")]
        public string Artist { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("tracks_source_id")]
        public int TracksSourceId { get; set; }

        [Column("genre")]
        public Genre Genre { get; set; }

        [Column("tempo")]
        public Tempo Tempo { get; set; }

        [Column("expiration_datetime")]
        public DateTime ExpirationDatetime { get; set; }

        public int RemainingLifeSeconds => (int)ExpirationDatetime.Subtract(DateTime.Now.AddSeconds(10)).TotalSeconds;
        
        public TracksSource TracksSource { get; set; }
    }
}
