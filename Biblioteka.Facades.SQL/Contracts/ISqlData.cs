using Biblioteka.Facades.SQL.Models;

namespace Biblioteka.Facades.SQL.Contracts
{
    public interface ISqlData
    {
        public List<Genre> GetAllGenres();
        public void EditBook(Book book, string genreName);
        public void EditGenre(Genre genre);
        public void RemoveGenre(int id);
        public void RemoveBook(int id);
        public int AddBookstore(string name);
        public int AddGenreToSql(Genre genre);
        public Book FindBook(int bookId);
        public List<Bookstore> ShowBookstores();
        public List<Bookstore> GetBookstores(string bookStoreName);
        public List<Book> GetBooks();
        public int AddBook(Book book, Bookstore bookstore);
        public void UpdateBookstore(Bookstore bookstore);
    }
}
