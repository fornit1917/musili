using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApi.Utils
{
    public static class RandomUtils
    {
        private static Random rnd = new Random();

        public static T GetRandomListItem<T>(List<T> items) {
            return items[rnd.Next(0, items.Count)];
        }

        public static int GetRandomFromInterval(int start, int end) {
            return rnd.Next(start, end);
        }

        public static List<T> GetRandomSlice<T>(List<T> items, int count) {
            if (count > items.Count) {
                count = items.Count;
            }
            int start = rnd.Next(0, items.Count);
            if (items.Count - start >= count) {
                return items.GetRange(start, count);
            } else {
                int firstCount = items.Count - start;
                return items.GetRange(start, firstCount).Concat(items.GetRange(0, count - firstCount)).ToList();
            }
        }
    }
}
