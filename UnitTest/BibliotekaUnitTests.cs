using Biblioteka.Facades.SQL;
using Biblioteka.Facades.SQL.Models;
using NUnit.Framework;

namespace BibliotekaUnitTests
{
    public class BibliotekaUnitTests
    {

        [Test]
        public void GetAllGenres()
        {
            //Arrange
            SqlFacade sqlFacade = new SqlFacade();
            List<Genre> result = new List<Genre>();

            //Act
            result = sqlFacade.GetAllGenres();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditBook()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            Book book = new Book { Name = "ImeKnjige" };
            Bookstore bookstore = new Bookstore { Name = "ImeKnjizare" };
            SqlFacade sqlFacade = new SqlFacade();
            int id = sqlFacade.AddBook(book, bookstore);
            book = new Book { Name = "EditedWithUnitTest" };
            Book originalBook = new Book();
            originalBook = sqlFacade.FindBook(id);

            //Act
            sqlFacade.EditBook(book, genre.Name);


            //Assert
            Assert.AreNotEqual(originalBook, book);
        }

        [Test]
        public void EditGenre()
        {
            //Arrange
            Genre genre = new Genre { Name = "ImeZanra" };
            SqlFacade sqlFacade = new SqlFacade();
            int id = sqlFacade.AddGenreToSql(genre);
            genre = new Genre { Name = "EditedGenreWithUT" };
            Genre originalGenre = new Genre();
            List<Genre> genreList = sqlFacade.GetAllGenres();

            //Act
            originalGenre = genreList.Where(x => x.Id == id).FirstOrDefault();
            sqlFacade.EditGenre(genre);

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
            Book book = new Book { Name = "ImeKnjige" };
            Bookstore bookstore = new Bookstore { Name = "ImeKnjizare" };
            SqlFacade sqlFacade = new SqlFacade();
            int id = sqlFacade.AddBook(book, bookstore);
            Book result = new Book();
            List<Book> bookList = new List<Book>();

            //Act
            sqlFacade.RemoveBook(id);
            result = bookList.Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.AreEqual(result.Deleted, "true");
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
            List<Genre> genreList = new List<Genre>();

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
            Book book = new Book { Name = "ImeKnjige" };
            Bookstore bookstore = new Bookstore { Name = "ImeKnjizare" };
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