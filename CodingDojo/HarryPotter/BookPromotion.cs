using System.Collections.Generic;

namespace HarryPotter
{
    public class BookPromotion
    {
        public List<Criteria> Criteria  { get; set; }
        public double Discount { get; set; }
    }

    public class Criteria
    {
        public List<List<BookPromotionItem>> List { get; set; }

        public List<BookPromotionItem> GetFirstValidCombination(Dictionary<int, int> currentBuying)
        {
            var currentBuyingDic = currentBuying;
            foreach (var element in List)
            {
                for (var i = 0; i < element.Count; i++)
                {
                    if (currentBuyingDic[element[i].BookId] < element[i].Quantity)
                    {
                        break;
                    }
                    if (i == element.Count - 1) return element;
                }

            }
            return null;
        }
    }
}