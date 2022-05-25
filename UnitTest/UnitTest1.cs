using Biblioteka.Facades.SQL;
using Biblioteka.Facades.SQL.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    public class Tests
    {
        Genre genre;
        Bookstore bookstore;
        Book book;
        SqlFacade sqlFacade;
        public Tests()
        {
            sqlFacade = new SqlFacade();
            genre = new Genre { Name = "ByUnitTest" };
            bookstore = new Bookstore { Name = "ByUnitTest", Id = 5};
            book = new Book { Name = "ByUnitTest", Genre = genre, Bookstore = bookstore };
        }

        [Test]
        public void GetAllGenres()
        {
            //Arrange

            //Act
            List<Genre> result = sqlFacade.GetAllGenres();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditBook()
        {
            //Arrange
            int id = sqlFacade.AddBook(book, bookstore);
            book = new Book { Name = "EditedWithUnitTest"};

            //Act
            Book originalBook = sqlFacade.FindBook(id);
            sqlFacade.EditBook(book,genre.Name);


            //Assert
            Assert.AreNotEqual(originalBook, book);
        }

        [Test]
        public void EditGenre()
        {
            //Arrange
            int id = sqlFacade.AddGenreToSql(genre);
            genre = new Genre {Name = "EditedGenreWithUT" };

            //Act
            Genre originalGenre = sqlFacade.GetAllGenres().Where(x => x.Id == id).FirstOrDefault();
            sqlFacade.EditGenre(genre);

            //Assert
            Assert.AreNotEqual(originalGenre, genre);
        }

        [Test]
        public void RemoveGenre()
        {
            //Arrange

            //Act
            int id = sqlFacade.AddGenreToSql(genre);
            sqlFacade.RemoveGenre(id);
            Genre result = sqlFacade.GetAllGenres().Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.AreEqual(result.Deleted,"true");
        }

        [Test]
        public void RemoveBook()
        {
            //Arrange

            //Act
            int id = sqlFacade.AddBook(book,bookstore);
            sqlFacade.RemoveBook(id);
            Book result = sqlFacade.GetBooks().Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.AreEqual(result.Deleted, "true");
        }

        [Test]
        public void AddBookstore()
        {
            //Arrange

            //Act
            int id = sqlFacade.AddBookstore(bookstore.Name);
            Bookstore result = sqlFacade.ShowBookstores().Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddGenreToSql()
        {
            //Arrange

            //Act
            int id = sqlFacade.AddGenreToSql(genre);
            Genre result = sqlFacade.GetAllGenres().Where(x => x.Id == id).FirstOrDefault();
            sqlFacade.RemoveGenre(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void FindBook()
        {
            //Arrange
            int bookId = 100;

            //Act
            Book result = sqlFacade.FindBook(bookId);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ShowBookstores()
        {
            //Arrange

            //Act
            List<Bookstore> result = sqlFacade.ShowBookstores();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetBookstores()
        {

        }

        [Test]
        public void GetBooks()
        {
            //Arrange

            //Act
            List<Book> result = sqlFacade.GetBooks();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddBook()
        {
            //Arrange

            //Act

            int id = sqlFacade.AddBook(book, bookstore);
            Book result = sqlFacade.GetBooks().Where(x => x.Id == id).FirstOrDefault();
            sqlFacade.RemoveBook(result.Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateBookstore()
        {
            //Arrange


            //Act


            //Assert

        }
    }
}