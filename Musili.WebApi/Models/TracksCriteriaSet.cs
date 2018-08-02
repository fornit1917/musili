using Musili.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApi.Models
{
    public class TracksCriteriaSet
    {
        private List<Tempo> tempos;
        private List<Genre> genres;

        private bool isAnyTempo;
        private bool isAnyGenre;

        public List<Tempo> Tempos => tempos;
        public List<Genre> Genres => genres;

        public bool IsAnyTempo => isAnyTempo;
        public bool IsAnyGenre => isAnyGenre;

        public TracksCriteriaSet(string temposCommaList, string genresCommaList) {
            tempos = EnumUtils.ParseEnumValuesList<Tempo>(temposCommaList, ',').Where(item => item != Tempo.Any).ToList();
            isAnyTempo = tempos.Count == 0 || (tempos.Count == 1 && tempos[0] == Tempo.Any) || (tempos.Count == Enum.GetValues(typeof(Tempo)).Length - 1);

            genres = EnumUtils.ParseEnumValuesList<Genre>(genresCommaList, ',').Where(item => item != Genre.Any).ToList();
            isAnyGenre = genres.Count == 0 || (genres.Count == 1 && genres[0] == Genre.Any) || (genres.Count == Enum.GetValues(typeof(Genre)).Length - 1);
        }

        public TracksCriteria GetRandomCriteria() {
            TracksCriteria criteria = new TracksCriteria();
            criteria.Genre = genres.Count > 0 ? RandomUtils.GetRandomListItem<Genre>(genres) : Genre.Any;
            criteria.Tempo = tempos.Count > 0 ? RandomUtils.GetRandomListItem<Tempo>(tempos) : Tempo.Any;
            return criteria;
        }
    }
}
