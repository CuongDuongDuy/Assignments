using System;
using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    static class Program
    {
        static void Main(string[] args)
        {
            int[] promotedBookIds = { 1, 2, 3, 4, 5 };

            var buyingBasket = new BuyingBasket
            {
                Items = new List<BuyingItem>
                {
                    new BuyingItem {BookId = 1, Quantity = 100},
                    new BuyingItem {BookId = 2, Quantity = 100},
                    new BuyingItem {BookId = 3, Quantity = 1},
                    new BuyingItem {BookId = 4, Quantity = 50},
                    new BuyingItem {BookId = 5, Quantity = 50}
                }
            };
            var bookPromotionCompaign = new HarryPotterPromotionCompaign(promotedBookIds);
            var calculator = new BookCalculatorService(bookPromotionCompaign, buyingBasket);
            var buyingCombinations = calculator.Calc();
            var sum = 0.0M;
            if (buyingCombinations != null)
            {
                foreach (var combination in buyingCombinations)
                {
                    foreach (var buyingSet in combination.Items)
                    {
                        Console.WriteLine("Buying Set: ");
                        var details = buyingSet.Items.Select(buyingItem => string.Format("({0} x {1})", buyingItem.BookId, buyingItem.Quantity)).Aggregate("", (current, info) => current + info);
                        Console.WriteLine(details);
                    }
                    sum += combination.GetTotal();
                    Console.WriteLine(combination.GetTotal());
                }
            }
            Console.WriteLine("Total {0}", sum);
            Console.ReadLine();
        }
    }
}
