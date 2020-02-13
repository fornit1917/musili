using Musili.ApiApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Musili.ApiApp.Models {
    public class TracksCriteria {
        public List<string[]> TagsGroups { get; }

        public TracksCriteria(string temposCommaList, string genresCommaList) {
            string[] tempos = temposCommaList?.Split(",").Where(x => !string.IsNullOrEmpty(x) && x != "any").Select(x => x).ToArray();
            string[] genres = genresCommaList?.Split(",").Where(x => !string.IsNullOrEmpty(x) && x != "any").Select(x => x).ToArray();
            
            TagsGroups = new List<string[]>();
            if (tempos?.Length > 0) {
                TagsGroups.Add(tempos);
            }
            if (genres?.Length > 0) {
                TagsGroups.Add(genres);
            }
        }

        public TracksCriteria(List<string[]> tagsGroups) {
            TagsGroups = tagsGroups;
        }

        public TracksCriteria(string tagsGroups) {
            TagsGroups = new List<string[]>();
            if (!string.IsNullOrWhiteSpace(tagsGroups)) {
                foreach (var tagsCommaList in tagsGroups.Split(';')) {
                    TagsGroups.Add(tagsCommaList.Split(','));
                }
            }
        }

        public TracksCriteria GetRandomMinimalCriteria() {
            List<string[]> minimalTagsGroups = TagsGroups.Select(tags => new[] { RandomUtils.GetRandomListItem(tags) }).ToList();
            return new TracksCriteria(minimalTagsGroups);
        }

        public override string ToString() {
            if (TagsGroups.Count == 0) {
                return "";
            }

            var groupsAsStrings = TagsGroups
                .Select(tags => tags.OrderBy(t => t))
                .Select(tags => string.Join(',', tags))
                .OrderBy(tagsStr => tagsStr);

            return string.Join(';', groupsAsStrings);
        }
    }
}
