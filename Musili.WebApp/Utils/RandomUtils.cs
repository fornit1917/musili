using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApp.Utils
{
    public static class RandomUtils
    {
        public static T GetRandomListItem<T>(List<T> items) {
            Random rnd = new Random();
            return items[rnd.Next(0, items.Count)];
        }
    }
}
