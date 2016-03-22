using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotter
{
    public class HappyPotterPromotionService
    {
        protected Dictionary<int, int> itemsBuying;
        private List<BookPromotion> promotions;

        public HappyPotterPromotionService(List<BookPromotion> promotions, IEnumerable<BookBuying> bookBuyings)
        {
            this.promotions = promotions;
            foreach (var bookBuying in bookBuyings)
            {
                itemsBuying.Add(bookBuying.BookId, bookBuying.Quantity);
            }
        }

        private void Processing()
        {
            var result = new List<BookSelling>();
            foreach (var bookPromotion in promotions)
            {
                if (CheckValidCombination(bookPromotion.BookId))
                {
                    result.Add(ApplyValidCombination(bookPromotion.BookId));
                }
            }
        }

        private bool CheckValidCombination(int[] bookIds)
        {
            foreach (var bookId in bookIds)
            {
                var quantityBuying = itemsBuying[bookId];
                if (quantityBuying== )
                {
                    return false;
                }
            }
            return true;
        }

        private BookSelling ApplyValidCombination(int[] bookIds)
        {
            foreach (var bookId in bookIds)
            {
                itemsBuying[bookId]--;
            }
            return new BookSelling
            {
                BookIds = bookIds
            };

        }

        public IEnumerable<BookSelling> GetBuyingCombinations()
        {
            var result = new List<BookSelling>();
            return result;
        }
    }
}
