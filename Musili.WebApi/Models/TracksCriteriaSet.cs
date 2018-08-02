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

        public TracksCriteriaSet(string temposCommaList, string genresCommaList) {
            tempos = EnumUtils.ParseEnumValuesList<Tempo>(temposCommaList, ',');
            genres = EnumUtils.ParseEnumValuesList<Genre>(genresCommaList, ',');
        }

        public TracksCriteria GetRandomCriteria() {
            TracksCriteria criteria = new TracksCriteria();
            criteria.Genre = genres.Count > 0 ? RandomUtils.GetRandomListItem<Genre>(genres) : Genre.Any;
            criteria.Tempo = tempos.Count > 0 ? RandomUtils.GetRandomListItem<Tempo>(tempos) : Tempo.Any;
            return criteria;
        }
    }
}
