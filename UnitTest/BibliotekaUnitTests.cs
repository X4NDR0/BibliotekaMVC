using Biblioteka.Facades.SQL;
using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Facades.SQL.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BibliotekaUnitTests
{
    public class BibliotekaUnitTests
    {
        private SqlFacade _sqlFacade;

        public BibliotekaUnitTests()
        {
            _sqlFacade = new SqlFacade();
        }

        [Test]
        public void GetAllGenres()
        {
            //Arrange
            int expectedResult = 32;

            //Act
            List<Genre> result = _sqlFacade.GetAllGenres();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), expectedResult);
        }

        [Test]
        public void EditBook()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            Bookstore bookstore = new Bookstore { Id = 1, Name = "ImeKnjizare" };
            Book book = new Book { Name = "ImeKnjige", Genre = genre, Bookstore = bookstore };
            book = new Book { Name = "EditedWithUnitTest" };

            //Act
            int id = _sqlFacade.AddBook(book, bookstore);
            Book originalBook = _sqlFacade.FindBook(id);
            _sqlFacade.EditBook(book, genre.Name);


            //Assert
            Assert.AreNotEqual(originalBook, book);
        }

        [Test]
        public void EditGenre()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            int id = _sqlFacade.AddGenreToSql(genre);
            genre = new Genre { Name = "EditedGenreWithUT" };
            Genre originalGenre = new Genre();
            List<Genre> genreList = _sqlFacade.GetAllGenres();

            //Act
            originalGenre = genreList.Where(x => x.Id == id).FirstOrDefault();
            _sqlFacade.EditGenre(genre);

            //Assert
            Assert.AreNotEqual(originalGenre, genre);
        }

        [Test]
        public void RemoveGenre()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            SqlFacade sqlFacade = new SqlFacade();
            int id = sqlFacade.AddGenreToSql(genre);
            Genre result = new Genre();
            List<Genre> genreList = sqlFacade.GetAllGenres();

            //Act

            sqlFacade.RemoveGenre(id);
            result = genreList.Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.AreEqual(result.Deleted, "true");
        }

        [Test]
        public void RemoveBook()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            Bookstore bookstore = new Bookstore { Id = 1, Name = "ImeKnjizare" };
            Book book = new Book { Name = "ImeKnjige", Genre = genre, Bookstore = bookstore };
            SqlFacade sqlFacade = new SqlFacade();
            int id = sqlFacade.AddBook(book, bookstore);
            Book result = new Book();
            List<Book> bookList = sqlFacade.GetBooks();

            //Act
            sqlFacade.RemoveBook(id);
            result = bookList.Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.AreSame("true", result.Deleted);
        }

        [Test]
        public void AddBookstore()
        {
            //Arrange
            SqlFacade sqlFacade = new SqlFacade();
            string bookstoreName = "ImeKnjizare";
            int id = sqlFacade.AddBookstore(bookstoreName);
            Bookstore result = new Bookstore();
            List<Bookstore> bookstoreList = sqlFacade.ShowBookstores();

            //Act
            result = bookstoreList.Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddGenreToSql()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            SqlFacade sqlFacade = new SqlFacade();
            int id = sqlFacade.AddGenreToSql(genre);
            Genre result = new Genre();
            List<Genre> genreList = sqlFacade.GetAllGenres();

            //Act
            result = genreList.Where(x => x.Id == id).FirstOrDefault();
            sqlFacade.RemoveGenre(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void FindBook()
        {
            //Arrange
            SqlFacade sqlFacade = new SqlFacade();
            Book result = new Book();
            int bookId = 100;

            //Act
            result = sqlFacade.FindBook(bookId);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ShowBookstores()
        {
            //Arrange
            SqlFacade sqlFacade = new SqlFacade();
            List<Bookstore> result = new List<Bookstore>();

            //Act
            result = sqlFacade.ShowBookstores();

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
            SqlFacade sqlFacade = new SqlFacade();
            List<Book> result = new List<Book>();

            //Act
            result = sqlFacade.GetBooks();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddBook()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            Bookstore bookstore = new Bookstore { Id = 1, Name = "ImeKnjizare" };
            Book book = new Book { Name = "ImeKnjige", Genre = genre, Bookstore = bookstore };
            SqlFacade sqlFacade = new SqlFacade();
            int id = sqlFacade.AddBook(book, bookstore);
            Book result = new Book();
            List<Book> bookList = sqlFacade.GetBooks();

            //Act
            result = bookList.Where(x => x.Id == id).FirstOrDefault();
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