using System.Collections.Generic;
using System.Linq;

namespace HarryPotter
{
    public class BookStore
    {
        public List<Book> Books = new List<Book>
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
        public List<BookPricing> BookPricing = new List<BookPricing>
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

        public List<BookPromotion> HarryPotterPromotion()
        {
            var promotedBooks = new[] {1, 2, 3, 4, 5};
            var promotedDiscounts = new Dictionary<int, double>()
            {
                {2,0.05},
                {3,0.10},
                {4,0.20},
                {5,0.25}

            };
            var result = new List<BookPromotion>();
            for (var i = 2; i <= 5; i++)
            {
                var availableCombinations = promotedBooks.Combinations(i);
                result.AddRange(GetPromotions(availableCombinations, promotedDiscounts[i]));
            }
            return result;
        }

        public List<BookPromotion> GetPromotions(IEnumerable<IEnumerable<int>> arrays, double discount)
        {
            var result = new List<BookPromotion>();
            foreach (var array in arrays)
            {
                var bookPromotion = new BookPromotion {Discount = discount};
                foreach (var bookPromotionItem in array.Select(i => new BookPromotionItem {BookId = i, Quantity = 1}))
                {
                    bookPromotion.Items.Add(bookPromotionItem);
                }
            }
            return result;
        }
    }
}
