using Biblioteka.Facades.SQL.Models;
using System.Collections.Generic;

namespace Biblioteka.Facades.SQL.Contracts
{
    public interface ISqlFacade
    {
        public List<Genre> GetAllGenres();
        public void EditBook(Book book);
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
        public Bookstore UpdateBookstore(Bookstore bookstore);
    }
}
