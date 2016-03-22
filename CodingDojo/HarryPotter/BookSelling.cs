using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace HarryPotter
{
    public class BookSelling
    {
        readonly BookStore bookStore = new BookStore();
        public double Discount { get; set; }
        public List<List<BookPromotionItem>> List { get; set; }

        public double Total()
        {
            var sum = List.SelectMany(criterion => criterion).Sum(combination => bookStore.BookPricing.First(x => x.BookId == combination.BookId).UnitPrice*combination.Quantity);
            return sum*(1 - Discount);
        }

        public void UpdateCurrentBuying(ref Dictionary<int, int> itemsBuying)
        {
            foreach (var combination in List.SelectMany(criterion => criterion))
            {
                itemsBuying[combination.BookId] -= combination.Quantity;
            }
        }
    }
}
