using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public class BuyingBookCombination
    {
        public List<BuyingSet> Items { get; set; }

        public decimal GetTotal()
        {
            var sum = 0.0M;

            foreach (var buyingSetWithDiscount in Items)
            {
                var subTotal = 0.0M;
                foreach (var buyingItem in buyingSetWithDiscount.Items)
                {
                    subTotal += BookStore.BookPricing.First(x => x.BookId == buyingItem.BookId).UnitPrice * buyingItem.Quantity *
                                (1 - buyingSetWithDiscount.Discount);
                }
                sum += subTotal;
            }
            return sum;
        }

        public BuyingBookCombination()
        {
            Items = new List<BuyingSet>();
        }
    }
}