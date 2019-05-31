using Musili.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApi.Models {
    public class TracksCriteria {
        public List<Tempo> Tempos { get; private set; }
        public List<Genre> Genres { get; private set; }

        public bool IsAnyTempo { get; private set; }
        public bool IsAnyGenre { get; private set; }

        public TracksCriteria(string temposCommaList, string genresCommaList) {
            Tempos = EnumUtils.ParseEnumValuesList<Tempo>(temposCommaList, ',').Where(item => item != Tempo.Any).ToList();
            IsAnyTempo = Tempos.Count == 0 || (Tempos.Count == 1 && Tempos[0] == Tempo.Any) || (Tempos.Count == Enum.GetValues(typeof(Tempo)).Length - 1);

            Genres = EnumUtils.ParseEnumValuesList<Genre>(genresCommaList, ',').Where(item => item != Genre.Any).ToList();
            IsAnyGenre = Genres.Count == 0 || (Genres.Count == 1 && Genres[0] == Genre.Any) || (Genres.Count == Enum.GetValues(typeof(Genre)).Length - 1);
        }

        public override string ToString() {
            string genres = IsAnyGenre ? "Any" : string.Join(",", Genres);
            string tempos = IsAnyGenre ? "Any" : string.Join(",", Tempos);
            return $"Genres: {genres} / Tempos: {tempos}";
        }
    }
}
