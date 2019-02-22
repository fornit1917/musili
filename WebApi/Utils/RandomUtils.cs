using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApi.Utils {
    public static class RandomUtils {
        private static Random rnd = new Random();

        public static T GetRandomListItem<T>(List<T> items) {
            return items[rnd.Next(0, items.Count)];
        }

        public static int GetRandomFromInterval(int start, int end) {
            return rnd.Next(start, end);
        }

        public static List<T> GetRandomItems<T>(List<T> items, int count) {
            if (count > items.Count) {
                count = items.Count;
            }

            HashSet<int> indexes = new HashSet<int>(count);
            List<T> result = new List<T>(count);
            for (int i = 0; i < count; i++) {
                int index = rnd.Next(0, items.Count);
                while (true) {
                    if (index >= items.Count) {
                        index = 0;
                    }
                    if (!indexes.Contains(index)) {
                        break;
                    }
                    index++;
                }

                indexes.Add(index);
                result.Add(items[index]);
            }

            return result;
        }
    }
}
