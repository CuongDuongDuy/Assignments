using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public class BuyingQuantityCombination
    {
        public List<int> QuantityBuying { get; set; }
        public decimal Discount { get; set; }

        public BuyingSet GetFirstAvailableBuyingCombination(List<BuyingSet> currentBuyingSets,
            List<BuyingItem> currentBuyingItems)
        {
            foreach (var buyingSet in currentBuyingSets)
            {
                var availableSet = buyingSet.CheckValid(currentBuyingItems);
                if (availableSet != null)
                {
                    return availableSet;
                }
            }
            return null;
        }

        public int ItemsNeeded
        {
            get { return QuantityBuying.Sum(); }
        }
    }
}