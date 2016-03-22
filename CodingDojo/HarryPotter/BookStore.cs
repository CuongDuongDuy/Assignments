using System.Collections.Generic;

namespace HarryPotter
{
    public class BookStore
    {
        public List<Book> Episode = new List<Book>
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
        public List<BookPromotion> Promotions = new List<BookPromotion>
        {
            new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },
            new BookPromotion
            {
                BookId = new [] {1,3},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },new BookPromotion
            {
                BookId = new [] {1,2},
                Discount = 5

            },
        };
    }
}
