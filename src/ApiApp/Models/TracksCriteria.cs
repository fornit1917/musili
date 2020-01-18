using System.Collections.Generic;
using System.Linq;

namespace Musili.ApiApp.Models {
    public class TracksCriteria {
        public string[] Tempos { get; }
        public string[] Genres { get; }

        public bool IsAnyTempo => Tempos == null || Tempos.Length == 0;
        public bool IsAnyGenre => Genres == null || Genres.Length == 0;

        public TracksCriteria(string temposCommaList, string genresCommaList) {
            Tempos = temposCommaList?.Split(",").Where(x => !string.IsNullOrEmpty(x)).Select(x => x).ToArray();
            Genres = genresCommaList?.Split(",").Where(x => !string.IsNullOrEmpty(x)).Select(x => x).ToArray();
        }
    }
}
