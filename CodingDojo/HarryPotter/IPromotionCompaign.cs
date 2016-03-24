using System.Collections.Generic;

namespace HarryPotter
{
    public interface IPromotionCompaign
    {
        Dictionary<int, decimal> Discounts();
        Dictionary<int, List<BuyingSet>> GetBuyingQuantityCombinations();
        int[] GetAvailableBuyingSets();
    }
}