using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public class BookCalculatorService
    {
        private readonly IPromotionCompaign promotionCompaign;
        private readonly BuyingBasket buyingBasket;
        public BookCalculatorService(IPromotionCompaign promotionCompaign, BuyingBasket buyingBasket)
        {
            this.promotionCompaign = promotionCompaign;
            this.buyingBasket = buyingBasket;
        }


        public List<BuyingBookCombination> Calc()
        {
            if (buyingBasket.ItemsCount == 0) return null;
            var result = new List<BuyingBookCombination>();
            foreach (var buyingQuantityCombination in BuyingQuantityCombinations)
            {
                if (buyingQuantityCombination.ItemsNeeded > buyingBasket.ItemsCount)
                {
                    continue;
                }
                var quantityBuying = buyingQuantityCombination.QuantityBuying;
                var buyingBookCombination = new BuyingBookCombination();
                for (var i = 0; i < quantityBuying.Count(); i++)
                {
                    var currentBuyingSets = promotionCompaign.GetBuyingQuantityCombinations()[quantityBuying[i]];
                    var buyingSet = buyingQuantityCombination.GetFirstAvailableBuyingCombination(currentBuyingSets,
                        buyingBasket.Items);
                    if (buyingSet == null)
                    {
                        foreach (var item in buyingBookCombination.Items)
                        {
                            buyingBasket.PutIn(item);
                        }
                        break;
                    }
                    buyingSet.Discount = promotionCompaign.Discounts()[buyingSet.Items.Count];
                    buyingBookCombination.Items.Add(buyingSet);
                    buyingBasket.GetOut(buyingSet);
                    if (i == quantityBuying.Count() - 1)
                    {
                        result.Add(buyingBookCombination);
                    }
                }
            }
            return result;
        }


        private List<BuyingQuantityCombination> buyingQuantityCombinations;
        public List<BuyingQuantityCombination> BuyingQuantityCombinations
        {
            get
            {
                if (buyingQuantityCombinations == null)
                {
                    buyingQuantityCombinations =
                        CombinationHelper.GetCombinations(promotionCompaign.GetAvailableBuyingSets(), buyingBasket.ItemsCount,
                            "").ToDiscountCombinations(promotionCompaign.Discounts());
                }
                return buyingQuantityCombinations;
            }
        }
    }
}