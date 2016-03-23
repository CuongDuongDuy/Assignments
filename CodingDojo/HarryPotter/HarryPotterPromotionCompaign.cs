using System.Collections.Generic;

namespace HarryPotter
{
    public class HarryPotterPromotionCompaign : IPromotionCompaign
    {

        private readonly int[] promotedBooks;
        public HarryPotterPromotionCompaign(int[] promotedBookIds)
        {
            promotedBooks = promotedBookIds;
        }

        private Dictionary<int, double> discounts = new Dictionary<int, double>()
        {
            {1, 0.0},
            {2, 0.05},
            {3, 0.1},
            {4, 0.2},
            {5, 0.25}
        };
        public Dictionary<int, double> Discounts()
        {
            return discounts;
        }

        private Dictionary<int, List<BuyingSet>> buyingQuantityCombinations;

        public Dictionary<int, List<BuyingSet>> GetBuyingQuantityCombinations()
        {
            if (buyingQuantityCombinations == null)
            {
                buyingQuantityCombinations = new Dictionary<int, List<BuyingSet>>();

                for (var i = 1; i <= 5; i++)
                {
                    var buyingSets = new List<BuyingSet>();
                    var availableSet = promotedBooks.Combinations(i);
                    foreach (var set in availableSet)
                    {
                        var newBuyingSet = new BuyingSet();
                        foreach (var item in set)
                        {
                            var newBuyingItem = new BuyingItem
                            {
                                BookId = item,
                                Quantity = 1
                            };
                            newBuyingSet.Items.Add(newBuyingItem);
                        }
                        buyingSets.Add(newBuyingSet);
                    }
                    buyingQuantityCombinations.Add(i, buyingSets);
                }
            }
            return buyingQuantityCombinations;
        }

        private List<int> availableBuyingSets;

        public int[] GetAvailableBuyingSets()
        {
            if (availableBuyingSets == null)
            {
                availableBuyingSets = new List<int>();
                for (int i = 1; i <= promotedBooks.Length; i++)
                {
                    availableBuyingSets.Add(i);
                }
            }
            return availableBuyingSets.ToArray();
        }
    }
}