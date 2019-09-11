using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public interface IBookService
    {
        Book GetDefaultBook();
        Book AddBook(Book book);
    }
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public Book AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new Exception("Cannot add book with no title.");
            }
            return bookRepository.Create(book);
        }

        public Book GetDefaultBook()
        {
            return bookRepository.Get(1);
        }
    }
}
