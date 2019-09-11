using Library;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitProject
{
    public class BookServiceTest
    {
        private BookService bookService;
        public BookServiceTest()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(b => b.Get(1)).Returns(new Book { Author = "Default Book Author", Title = "Default Book", Id = 1, Pages = 100 });
            bookRepositoryMock.Setup(b => b.Create(It.IsAny<Book>())).Returns<Book>(b => b);
            bookService = new BookService(bookRepositoryMock.Object);
        }

        [Fact]
        public void GetDefaultBook()
        {
            var defaultBook = bookService.GetDefaultBook();
            Assert.NotNull(defaultBook);
            Assert.Equal("Default Book", defaultBook.Title);
            Assert.Equal("Default Book Author", defaultBook.Author);
            Assert.Equal(1, defaultBook.Id);
            Assert.Equal(100, defaultBook.Pages);
        }

        [Fact]
        public void CreateBookWithNoTitle()
        {
            // 
            Assert.Throws<Exception>(
                () => bookService.AddBook(new Book { Title = "" })
                );
        }

        [Theory]
        [InlineData("A Game of Thrones", "George RR Martin", 1000)]
        [InlineData("Mistborn", "Brandon Sanderson", 600)]
        public void CreateBook(string title, string author, int pages)
        {
            var bookToAdd = bookService.AddBook(new Book
            {
                Title = title,
                Author = author,
                Pages = pages
            });
            var addedBook = bookService.AddBook(bookToAdd);

            Assert.NotNull(addedBook);
            Assert.Equal(bookToAdd.Title, addedBook.Title);
            Assert.Equal(bookToAdd.Author, addedBook.Author);
            Assert.Equal(bookToAdd.Pages, addedBook.Pages);
        }
    }
}
