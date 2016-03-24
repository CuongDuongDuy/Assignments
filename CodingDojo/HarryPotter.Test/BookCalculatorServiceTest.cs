using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace HarryPotter.Test
{
    [TestFixture]
    public class BookCalculatorServiceTest
    {
        private IPromotionCompaign promotionCompaign;
        private BuyingBasket buyingBasket;

        private Mock<IPromotionCompaign> promotionCompaignMock;
        private BookCalculatorService bookCalculatorService;

        [SetUp]
        public void Initial()
        {
        }

        [TestMethod]
        public void BuyingQuantityCombinations_Returns_Expected_List_Of_BuyingQuantityCombination()
        {
            var buyingBasket = new BuyingBasket
            {
                Items = new List<BuyingItem>
                {
                    new BuyingItem {BookId = 1, Quantity = 2},
                    new BuyingItem {BookId = 2, Quantity = 2},
                    new BuyingItem {BookId = 3, Quantity = 2},
                    new BuyingItem {BookId = 4, Quantity = 1},
                    new BuyingItem {BookId = 5, Quantity = 1}
                }
            };
            var expectedAvailableBuyingSets = new[] {1, 2, 3, 4, 5};
            promotionCompaignMock.Setup(x => x.GetAvailableBuyingSets()).Returns(expectedAvailableBuyingSets);

            var expectedDiscounts = new Dictionary<int, double>()
            {
                {1, 0.0},
                {2, 0.05},
                {3, 0.1},
                {4, 0.2},
                {5, 0.25}
            };

            promotionCompaignMock.Setup(x => x.Discounts()).Returns(expectedDiscounts);

            bookCalculatorService = new BookCalculatorService(promotionCompaignMock.Object, buyingBasket);

            var expected = new List<BuyingQuantityCombination>
            {
                new BuyingQuantityCombination
                {
                    Discount = 0.0,
                    QuantityBuying = new List<int> {1,2,3 }
                   //ToDO

                }
            };
            var actual = bookCalculatorService.BuyingQuantityCombinations;
            Assert.AreEqual(expected, actual);
        }
    }
}
