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
                    enumerable.Skip(i).Combinations(k - 1).Select(c => (new[] { e }).Concat<T>(c)));
        }

        public static IEnumerable<IEnumerable<T>> CombinationsNotRepeat<T>(this IEnumerable<T> elements, int k)
        {
            var enumerable = elements as T[] ?? elements.ToArray();
            return k == 0 ? new[] { new T[0] } :
                enumerable.SelectMany((e, i) =>
                    enumerable.Skip(i + 1).CombinationsNotRepeat(k - 1).Select(c => (new[] { e }).Concat<T>(c)));
        }

        public static List<BuyingQuantityCombination> ToDiscountCombinations(this IEnumerable<IEnumerable<int>> combinations,
            Dictionary<int, decimal> discounts)
        {
            var result = new List<BuyingQuantityCombination>();
            foreach (var combination in combinations)
            {
                var totalDiscount = 0.0M;
                var currentBuyings = combination as int[] ?? combination.ToArray();
                foreach (var currentBuying in currentBuyings)
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
                    QuantityBuying = currentBuyings.ToList(),
                    Discount = totalDiscount
                };
                result.Add(buyingCombination);
            }
            return result;
        }
    }
}