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
            rnd = new Random();
            return items[rnd.Next(0, items.Count)];
        }

        public static int GetRandomFromInterval(int start, int end) {
            return rnd.Next(start, end);
        }
    }
}
