using System.Collections.Generic;

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

        private List<BookSelling> Processing()
        {
            var result = new List<BookSelling>();
            foreach (var bookPromotion in promotions)
            {
                var temp = CheckValidCriteria(bookPromotion.Criteria);
                if (temp != null)
                {
                    var boolSelling = new BookSelling
                    {
                        List = temp,
                        Discount = bookPromotion.Discount
                    };
                    result.Add(boolSelling);
                    boolSelling.UpdateCurrentBuying(ref itemsBuying);
                }

            }
            return result;
        }

        private List<List<BookPromotionItem>> CheckValidCriteria(List<Criteria> criteria )
        {
            var result = new List<List<BookPromotionItem>>();
            foreach (var criterion in criteria)
            {
                var temp = criterion.GetFirstValidCombination(itemsBuying);
                if (temp == null)
                {
                    return null;
                }
                else
                {
                    result.Add(temp);
                }
            }
            return result;
        }

        public IEnumerable<BookSelling> GetBuyingCombinations()
        {
            var result = Processing();

            return result;
        }

    }
}
