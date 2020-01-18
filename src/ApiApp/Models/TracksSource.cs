using System.ComponentModel.DataAnnotations.Schema;

namespace Musili.ApiApp.Models {
    [Table("tracks_source", Schema = "app")]
    public class TracksSource {
        [Column("id")]
        public int Id { get; set; }

        [Column("service")]
        public TracksSourceService Service { get; set; }

        [Column("source_type")]
        public TracksSourceType SourceType { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("params_json")]
        public string ParamsJson { get; set; }

        [Column("tags")]
        public string[] Tags { get; set; }
    }
}