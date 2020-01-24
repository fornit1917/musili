using System.Collections.Generic;
using System.Linq;

namespace Musili.ApiApp.Models {
    public class TracksCriteria {
        public List<string[]> TagsGroups { get; }

        public TracksCriteria(string temposCommaList, string genresCommaList) {
            string[] tempos = temposCommaList?.Split(",").Where(x => !string.IsNullOrEmpty(x)).Select(x => x).ToArray();
            string[] genres = genresCommaList?.Split(",").Where(x => !string.IsNullOrEmpty(x)).Select(x => x).ToArray();
            
            TagsGroups = new List<string[]>();
            if (tempos?.Length > 0) {
                TagsGroups.Add(tempos);
            }
            if (genres?.Length > 0) {
                TagsGroups.Add(genres);
            }
        }
    }
}
