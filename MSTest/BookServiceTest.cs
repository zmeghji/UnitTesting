using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTest
{
    [TestClass]
    public class BookServiceTest
    {
        private BookService bookService;

        [TestInitialize]
        public void InitializeTest()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(b => b.Get(1)).Returns(new Book { Author = "Default Book Author", Title = "Default Book", Id = 1, Pages = 100 });
            bookRepositoryMock.Setup(b => b.Create(It.IsAny<Book>())).Returns<Book>(b => b);
            bookService = new BookService (bookRepositoryMock.Object);
        }
        [TestMethod]
        public void GetDefaultBook()
        {
            var defaultBook = bookService.GetDefaultBook();
            Assert.IsNotNull(defaultBook);
            Assert.AreEqual("Default Book", defaultBook.Title);
            Assert.AreEqual("Default Book Author", defaultBook.Author);
            Assert.AreEqual(1, defaultBook.Id);
            Assert.AreEqual(100, defaultBook.Pages);
        }
        [TestMethod]
        public void CreateBookWithNoTitle()
        {
            // 
            Assert.ThrowsException<Exception>(
                ()=> bookService.AddBook(new Book { Title = "" })
                );
        }
        [DataTestMethod]
        [DataRow("A Game of Thrones", "George RR Martin", 1000)]
        [DataRow("Mistborn", "Brandon Sanderson", 600)]
        public void CreateBook(string title, string author, int pages)
        {
            var bookToAdd = bookService.AddBook(new Book
            {
                Title =title,
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
