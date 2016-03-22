using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public static class CombinationHelper
    {
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            var enumerable = elements as T[] ?? elements.ToArray();
            return k == 0 ? new[] { new T[0] } :
              enumerable.SelectMany((e, i) =>
                enumerable.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }
}
