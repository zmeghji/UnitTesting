using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public interface IBookRepository
    {
        Book Get(int id);
        Book Create(Book book);
    }
    public class BookRepository : IBookRepository
    {
        private List<Book> Books;
        public BookRepository()
        {
            Books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Default Book",
                    Author = "Default Book Author",
                    Pages = 100
                }
            };
        }

        public Book Get(int id)
        {
            return Books.First(b => b.Id == id);
        }
        public Book Create(Book book)
        {
            book.Id= Books.Max(b => b.Id) + 1;
            Books.Add(book);
            return book;
        }
    }
}
