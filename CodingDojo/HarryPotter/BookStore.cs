using System.Collections.Generic;

namespace HarryPotter
{
    public class BookStore
    {
        public static List<BookPricing> BookPricing = new List<BookPricing>
        {
            new BookPricing
            {
                BookId = 1,
                UnitPrice = 8.0M
            },
            new BookPricing
            {
                BookId = 2,
                UnitPrice = 8.0M
            },
            new BookPricing
            {
                BookId = 3,
                UnitPrice = 8.0M
            },
            new BookPricing
            {
                BookId = 4,
                UnitPrice = 8.0M
            },
            new BookPricing
            {
                BookId = 5,
                UnitPrice = 8.0M
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
}