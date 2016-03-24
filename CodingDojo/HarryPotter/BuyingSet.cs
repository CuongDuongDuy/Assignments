using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public class BuyingSet
    {

        public decimal Discount { get; set; }
        public List<BuyingItem> Items { get; set; }

        public BuyingSet()
        {
            Items = new List<BuyingItem>();
            Discount = 0.0M;
        }

        public BuyingSet CheckValid(List<BuyingItem> currentBuying)
        {
            var currentBuyingDictionary = currentBuying.ToDictionary(buyingItem => buyingItem.BookId, buyingItem => buyingItem.Quantity);
            foreach (var item in Items)
            {
                if (currentBuyingDictionary[item.BookId] < item.Quantity)
                {
                    return null;
                }
            }
            return this;
        }
    }
}