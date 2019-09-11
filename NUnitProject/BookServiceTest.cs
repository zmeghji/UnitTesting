using Library;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitProject
{
    public class BookServiceTest
    {
        private BookService bookService;

        [SetUp]
        public void Setup()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(b => b.Get(1)).Returns(new Book { Author = "Default Book Author", Title = "Default Book", Id = 1, Pages = 100 });
            bookRepositoryMock.Setup(b => b.Create(It.IsAny<Book>())).Returns<Book>(b => b);
            bookService = new BookService(bookRepositoryMock.Object);
        }

        [Test]
        public void GetDefaultBook()
        {
            var defaultBook = bookService.GetDefaultBook();
            Assert.IsNotNull(defaultBook);
            Assert.AreEqual("Default Book", defaultBook.Title);
            Assert.AreEqual("Default Book Author", defaultBook.Author);
            Assert.AreEqual(1, defaultBook.Id);
            Assert.AreEqual(100, defaultBook.Pages);
        }

        [Test]
        public void CreateBookWithNoTitle()
        {
            Assert.Throws<Exception>(
                () => bookService.AddBook(new Book { Title = "" })
                );
        }

        [TestCase("A Game of Thrones", "George RR Martin", 1000)]
        [TestCase("Mistborn", "Brandon Sanderson", 600)]
        public void CreateBook(string title, string author, int pages)
        {
            var bookToAdd = bookService.AddBook(new Book
            {
                Title = title,
                Author = author,
                Pages = pages
            });
            var addedBook = bookService.AddBook(bookToAdd);

            Assert.IsNotNull(addedBook);
            Assert.AreEqual(bookToAdd.Title, addedBook.Title);
            Assert.AreEqual(bookToAdd.Author, addedBook.Author);
            Assert.AreEqual(bookToAdd.Pages, addedBook.Pages);
        }
    }
}
