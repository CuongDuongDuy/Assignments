using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Example
{
    public static class CombinationHelper
    {
        public static IEnumerable<string> GetCombinations(int[] set, int sum, string values)
        {
            for (int i = 0; i < set.Length; i++)
            {
                int left = sum - set[i];
                string vals = set[i] + "," + values;
                if (left == 0)
                {
                    yield return vals;
                }
                else
                {
                    int[] possible = set.Where(n => n <= sum).ToArray();
                    if (possible.Length > 0)
                    {
                        foreach (string s in GetCombinations(possible, left, vals))
                        {
                            yield return s;
                        }
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            var enumerable = elements as T[] ?? elements.ToArray();
            return k == 0 ? new[] { new T[0] } :
                enumerable.SelectMany((e, i) =>
                    enumerable.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat<T>(c)));
        }

        public static List<BuyingQuantityCombination> ToDiscountCombinations(this IEnumerable<string> combinations,
            Dictionary<int, double> discounts)
        {
            var result = new List<BuyingQuantityCombination>();
            foreach (var combination in combinations)
            {
                var currentBuyingCombination =
                    combination.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Int32.Parse(x))
                        .ToList();
                var totalDiscount = 0.0;
                foreach (var currentBuying in currentBuyingCombination)
                {
                    var currentDiscount = 0.0;
                    var found = discounts.TryGetValue(currentBuying, out currentDiscount);
                    if (found)
                    {
                        totalDiscount += currentDiscount;
                    }
                }
                var buyingCombination = new BuyingQuantityCombination
                {
                    QuantityBuying = currentBuyingCombination,
                    Discount = totalDiscount
                };
                result.Add(buyingCombination);
            }
            return result.OrderByDescending(x => x.Discount).ToList();
        }
    }
    static class Program
    {
        
        static void Main(string[] args)
        {
            int[] promotedBookIds = { 1, 2, 3, 4, 5 };

            var buyingBasket = new BuyingBasket
            {
                Items = new List<BuyingItem>
                {
                    new BuyingItem {BookId = 1, Quantity = 4},
                    new BuyingItem {BookId = 2, Quantity = 3},
                    new BuyingItem {BookId = 3, Quantity = 3},
                    new BuyingItem {BookId = 4, Quantity = 2},
                    new BuyingItem {BookId = 5, Quantity = 3}
                }
            };

           
            var bookPromotionCompaign = new HarryPotterPromotionCompaign(promotedBookIds);
            var calculator = new BookCalculatorService(bookPromotionCompaign, buyingBasket);
            var buying = calculator.Calc();
            foreach (var group in buying)
            {
                Console.WriteLine(group.Total());
            }

            Console.ReadLine();
        }
    }

    public class BuyingBasket
    {
        public List<BuyingItem> Items { get; set; }

        public int ItemsCount
        {
            get { return Items.Sum(x => x.Quantity); }
        }

        public void Action(BuyingSet buyingSet,int negative = -1 )
        {
            foreach (var buyingItem in buyingSet.Items)
            {
                var item = Items.First(x => x.BookId == buyingItem.BookId);
                item.Quantity += negative * buyingItem.Quantity;
            }
        }
    }


    public class BuyingQuantityCombination
    {
        public List<int> QuantityBuying { get; set; }
        public double Discount { get; set; }

        public BuyingSet GetFirstAvailableBuyingCombination(List<BuyingSet> currentBuyingSets, List<BuyingItem> currentBuyingItems )
        {
            foreach (var i in QuantityBuying)
            {
                foreach (var buyingSet in currentBuyingSets)
                {
                    var availableSet = buyingSet.CheckValid(currentBuyingItems);
                    if (availableSet != null)
                    {
                        return availableSet;
                    }
                }
            }
            return null;
        }

        public int ItemsNeeded
        {
            get { return QuantityBuying.Sum(); } 
        }
    }

    public class BuyingBookCombination
    {
        public List<BuyingSetWithDiscount> Items { get; set; }

        public double Total()
        {
            var sum = 0.0;

            foreach (var buyingSetWithDiscount in Items)
            {
                var subTotal = 0.0;
                foreach (var buyingItem in buyingSetWithDiscount.Items)
                {
                    subTotal+= BookStore.BookPricing.First(x => x.BookId == buyingItem.BookId).UnitPrice*buyingItem.Quantity*
                           (1 - buyingSetWithDiscount.Discount);
                }
                sum += subTotal;
            }
            return sum;
        }

        public BuyingBookCombination()
        {
            Items = new List<BuyingSetWithDiscount>();
        }
    }

    public class BuyingItem
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

    }

    public class BookStore
    {
        public static List<BookPricing> BookPricing = new List<BookPricing>
        {
            new BookPricing
            {
                BookId = 1,
                UnitPrice = 8.0
            },
            new BookPricing
            {
                BookId = 2,
                UnitPrice = 8.0
            },
            new BookPricing
            {
                BookId = 3,
                UnitPrice = 8.0
            },
            new BookPricing
            {
                BookId = 4,
                UnitPrice = 8.0
            },
            new BookPricing
            {
                BookId = 5,
                UnitPrice = 8.0
            },
        };
        public static List<Book> Books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Name = "Book 1"
            },
            new Book
            {
                Id = 2,
                Name = "Book 2"
            },
            new Book
            {
                Id = 3,
                Name = "Book 3"
            },
            new Book
            {
                Id = 4,
                Name = "Book 4"
            },
            new Book
            {
                Id = 5,
                Name = "Book 5"
            },
        };
    }
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BookPricing
    {
        public int BookId { get; set; }
        public double UnitPrice { get; set; }
    }

    public class HarryPotterPromotionCompaign:IPromotionCompaign
    {

        private readonly int[] promotedBooks;
        public HarryPotterPromotionCompaign(int[] promotedBookIds)
        {
            promotedBooks = promotedBookIds;
        }

        private Dictionary<int, double> discounts = new Dictionary<int, double>()
        {
            {1, 0.0},
            {2, 0.05},
            {3, 0.1},
            {4, 0.2},
            {5, 0.25}
        };
        public Dictionary<int, double> Discounts()
        {
            return discounts;
        }


        public Dictionary<int, List<BuyingSet>> GetBuyingQuantityCombinations()
        {
            var result = new Dictionary<int, List<BuyingSet>>();
          
            for (var i = 1; i <= 5; i++)
            {
                var buyingSets = new List<BuyingSet>();
                var availableSet = promotedBooks.Combinations(i);
                foreach (var set in availableSet)
                {
                    var newBuyingSet = new BuyingSet();
                    foreach (var item in set)
                    {
                        var newBuyingItem = new BuyingItem
                        {
                            BookId = item,
                            Quantity = 1
                        };
                        newBuyingSet.Items.Add(newBuyingItem);
                    }
                    buyingSets.Add(newBuyingSet);
                }
                result.Add(i, buyingSets);
            }
            return result;
        }

        public int[] GetPromotedBooks()
        {
            return promotedBooks;
        }
    }

    public interface IPromotionCompaign
    {
        Dictionary<int, double> Discounts();
        Dictionary<int, List<BuyingSet>> GetBuyingQuantityCombinations();

        int[] GetPromotedBooks();

    }

    public class BuyingSet
    {
        public List<BuyingItem> Items { get; set; }

        public BuyingSet()
        {
            Items = new List<BuyingItem>();
        }

        public BuyingSet CheckValid(List<BuyingItem> currentBuying )
        {
            var currentBuyingDictionary = currentBuying.ToDictionary(buyingItem => buyingItem.BookId, buyingItem => buyingItem.Quantity);
            foreach (var item in Items)
            {
                if (currentBuyingDictionary[item.BookId] < item.Quantity)
                {
                    return null;
                }
            }
            return this;
        }
    }

    public class BuyingSetWithDiscount : BuyingSet
    {
        public double Discount { get; set; }
    }

    public class BookCalculatorService
    {
        private IPromotionCompaign promotionCompaign;
        private BuyingBasket buyingBasket;
        public BookCalculatorService(IPromotionCompaign promotionCompaign, BuyingBasket buyingBasket)
        {
            this.promotionCompaign = promotionCompaign;
            this.buyingBasket = buyingBasket;
        }


        public List<BuyingBookCombination> Calc()
        {
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
                            buyingBasket.Action(item, +1);
                        }
                        break;
                    }
                    var buyingSetWDiscount = new BuyingSetWithDiscount
                    {
                        Discount = promotionCompaign.Discounts()[buyingSet.Items.Count],
                        Items = buyingSet.Items
                    };
                    buyingBookCombination.Items.Add(buyingSetWDiscount);
                    buyingBasket.Action(buyingSet);
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
                        CombinationHelper.GetCombinations(promotionCompaign.GetPromotedBooks(), buyingBasket.ItemsCount,
                            "").ToDiscountCombinations(promotionCompaign.Discounts());
                }
                return buyingQuantityCombinations;
            }
        } 
    }
}
