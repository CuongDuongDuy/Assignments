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

        private Dictionary<int, decimal> discounts = new Dictionary<int, decimal>()
        {
            {1, 0.00M},
            {2, 0.05M},
            {3, 0.10M},
            {4, 0.20M},
            {5, 0.25M}
        };
        public Dictionary<int, decimal> Discounts()
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