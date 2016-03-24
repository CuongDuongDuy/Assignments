using System;
using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public static class CombinationHelper
    {
        public static IEnumerable<string> GetCombinations(int[] set, int sum, string values)
        {
            for (var i = 0; i < set.Length; i++)
            {
                List<int> excluding = new List<int>();
                for (var j = 0; j < i; j++)
                {
                    excluding.Add(set[j]);
                }
                var left = sum - set[i];
                var vals = set[i] + "," + values;
                if (left == 0)
                {
                    yield return vals;
                }
                else
                {
                    var possible = set.Where(n => n <= sum && !excluding.Contains(n)).ToArray();
                    if (possible.Length > 0)
                    {
                        foreach (var s in GetCombinations(possible, left, vals))
                        {
                            yield return s;
                        }
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            var enumerable = elements as T[] ?? elements.ToArray();
            return k == 0 ? new[] { new T[0] } :
                enumerable.SelectMany((e, i) =>
                    enumerable.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat<T>(c)));
        }

        public static List<BuyingQuantityCombination> ToDiscountCombinations(this IEnumerable<string> combinations,
            Dictionary<int, decimal> discounts)
        {
            var result = new List<BuyingQuantityCombination>();
            foreach (var combination in combinations)
            {
                var currentBuyingCombination =
                    combination.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList();
                var totalDiscount = 0.0M;
                foreach (var currentBuying in currentBuyingCombination)
                {
                    var currentDiscount = 0.0M;
                    var found = discounts.TryGetValue(currentBuying, out currentDiscount);
                    if (found)
                    {
                        totalDiscount += currentDiscount;
                    }
                }
                var buyingCombination = new BuyingQuantityCombination
                {
                    QuantityBuying = currentBuyingCombination,
                    Discount = totalDiscount
                };
                result.Add(buyingCombination);
            }
            return result.OrderByDescending(x => x.Discount).ToList();
        }
    }
}