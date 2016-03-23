using System.Collections.Generic;

namespace HarryPotter
{
    public interface IPromotionCompaign
    {
        Dictionary<int, double> Discounts();
        Dictionary<int, List<BuyingSet>> GetBuyingQuantityCombinations();
        int[] GetAvailableBuyingSets();
    }
}