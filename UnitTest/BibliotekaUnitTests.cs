using Biblioteka.Facades.SQL;
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
            Book newBook = new Book { Name = "ImeKnjige", Genre = genre, Bookstore = bookstore };
            string nameChange = "EditedWithUnitTest";

            //Act
            int id = _sqlFacade.AddBook(newBook, bookstore);
            Book originalBook = _sqlFacade.FindBook(id);
            newBook.Name = nameChange;
            _sqlFacade.EditBook(newBook);


            //Assert
            Assert.AreNotEqual(originalBook.Name, newBook.Name);
        }

        [Test]
        public void EditGenre()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            string nameChange = "EditedGenreWithUT";


            //Act
            int id = _sqlFacade.AddGenreToSql(genre);
            List<Genre> genreList = _sqlFacade.GetAllGenres();
            Genre originalGenre = genreList.Where(x => x.Id == id).FirstOrDefault();
            genre.Name = nameChange;
            _sqlFacade.EditGenre(genre);

            //Assert
            Assert.AreNotEqual(originalGenre.Name, genre.Name);
        }

        [Test]
        public void RemoveGenre()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };

            //Act
            int id = _sqlFacade.AddGenreToSql(genre);
            _sqlFacade.RemoveGenre(id);
            List<Genre> genreList = _sqlFacade.GetAllGenres();
            Genre result = genreList.Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.AreEqual("true", result.Deleted);
        }

        [Test]
        public void RemoveBook()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            Bookstore bookstore = new Bookstore { Id = 1, Name = "ImeKnjizare" };
            Book book = new Book { Name = "ImeKnjige", Genre = genre, Bookstore = bookstore };

            //Act
            int id = _sqlFacade.AddBook(book, bookstore);
            _sqlFacade.RemoveBook(id);
            List<Book> bookList = _sqlFacade.GetBooks();
            Book result = bookList.Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.AreEqual("true", result.Deleted);
        }

        [Test]
        public void AddBookstore()
        {
            //Arrange
            string bookstoreName = "ImeKnjizare";

            //Act
            int id = _sqlFacade.AddBookstore(bookstoreName);
            List<Bookstore> bookstoreList = _sqlFacade.ShowBookstores();
            Bookstore result = bookstoreList.Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddGenreToSql()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };

            //Act
            int id = _sqlFacade.AddGenreToSql(genre);
            List<Genre> genreList = _sqlFacade.GetAllGenres();
            Genre result = genreList.Where(x => x.Id == id).FirstOrDefault();
            _sqlFacade.RemoveGenre(id);

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

            //Act
            List<Bookstore> result = _sqlFacade.ShowBookstores();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetBookstores()
        {
            //Arrange
            string bookStoreName = "Vulkan";

            //Act
            List<Bookstore> bookstoreList = _sqlFacade.GetBookstores(bookStoreName);

            //Arrange
            Assert.IsNotNull(bookstoreList[0].Books);
        }

        [Test]
        public void GetBooks()
        {
            //Arrange

            //Act
            List<Book> result = _sqlFacade.GetBooks();

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

            //Act
            int id = _sqlFacade.AddBook(book, bookstore);
            List<Book> bookList = _sqlFacade.GetBooks();
            Book result = bookList.Where(x => x.Id == id).FirstOrDefault();
            _sqlFacade.RemoveBook(result.Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateBookstore()
        {
            //Arrange
            Book book = new Book { Id = 127,Name = "Test"};
            List<Book> bookList = new List<Book>();
            bookList.Add(book);
            Bookstore bookstore = new Bookstore { Name = "Vulkan",Books = bookList,};

            //Act
            Bookstore expectedResult = _sqlFacade.UpdateBookstore(bookstore);

            //Assert
            Assert.IsTrue(expectedResult.Books.Count > 0);
        }
    }
}