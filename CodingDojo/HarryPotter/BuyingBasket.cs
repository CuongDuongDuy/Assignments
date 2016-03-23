using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public class BuyingBasket
    {
        public List<BuyingItem> Items { get; set; }

        public int ItemsCount
        {
            get { return Items.Sum(x => x.Quantity); }
        }

        private void Action(BuyingSet buyingSet, bool negative)
        {
            foreach (var buyingItem in buyingSet.Items)
            {
                var item = Items.First(x => x.BookId == buyingItem.BookId);
                item.Quantity += (negative ? -1 : 1) * buyingItem.Quantity;
            }
        }

        public void PutIn(BuyingSet buyingSet)
        {
            Action(buyingSet, false);
        }

        public void GetOut(BuyingSet buyingSet)
        {
            Action(buyingSet, true);
        }
    }
}