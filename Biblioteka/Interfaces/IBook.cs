using Biblioteka.Facades.SQL.Models;
using Biblioteka.ViewModels;
using System.Collections.Generic;

namespace Biblioteka.Interfaces
{
    public interface IBookService
    {
        public List<Book> SortBooksByNameAscending();
        public List<Book> SortBooksByNameDescending();
        public List<Book> SortBooksByPriceAscending();
        public List<Book> SortBooksByPriceDescending();
        public List<Book> DisplayAllBooks(int sortOption);
        public void EditBook(Book book, string genreName);
        public EditViewModel EditBook(int id);
        public List<Book> GetBooks();
        public AddBookViewModel AddBook();
        public void RemoveBook(int id);
        public void AddBook(Book book, string genreName, string bookStoreName);
        public SortBooksByGenreViewModel SortBooksByGenre();
        public SortBooksByGenreViewModel SortBooksByGenre(string genreName);
    }
}
