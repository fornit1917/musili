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
        
        public TracksSource TracksSource { get; set; }
    }
}
