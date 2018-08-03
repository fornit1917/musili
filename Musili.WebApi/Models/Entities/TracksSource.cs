using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Musili.WebApi.Models;

namespace Musili.WebApi.Models.Entities
{
    [Table("tracks_source", Schema="app")]
    public class TracksSource
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("genre")]
        public Genre Genre { get; set; }

        [Column("tempo")]
        public Tempo Tempo { get; set; }        

        [Column("service")]
        public TracksSourceService Service { get; set; }

        [Column("source_type")]
        public TracksSourceType SourceType { get; set; }        

        [Column("value")]
        public string Value { get; set; }

        [Column("params_json")]
        public string ParamsJson { get; set; }
    }
}