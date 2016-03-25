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
                var quantityBuying = buyingQuantityCombination.QuantityBuying;
                var moveToNextbuyingQuantityCombination = false;
                while (buyingBasket.ItemsCount >= buyingQuantityCombination.ItemsNeeded)
                {
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
                            moveToNextbuyingQuantityCombination = true;
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
                    if (moveToNextbuyingQuantityCombination)
                    {
                        break;
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
                    buyingQuantityCombinations = new List<BuyingQuantityCombination>();
                    for (int i = 1; i <= 2; i++)
                    {
                        var buyingQuantityCombination = promotionCompaign.GetAvailableBuyingSets().Combinations(i);

                        buyingQuantityCombinations.AddRange(
                            buyingQuantityCombination.ToDiscountCombinations(promotionCompaign.Discounts()));
                    }
                }
                return buyingQuantityCombinations.OrderByDescending(x=>x.Discount).ToList();
            }
        }
    }
}