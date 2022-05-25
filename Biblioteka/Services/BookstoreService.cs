using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Facades.SQL.Models;
using Biblioteka.Interfaces;
using System.Collections.Generic;

namespace Biblioteka.Services
{
    public class BookstoreService : IBookstoreService
    {
        private ISqlFacade _sqlService;

        public BookstoreService(ISqlFacade sqlService)
        {
            _sqlService = sqlService;
        }

        public Bookstore FindBookstore(string bookStoreName)
        {
            List<Bookstore> bookstores = GetBookstores(bookStoreName);
            Bookstore bookStore = bookstores[0];
            return bookStore;
        }

        public List<Bookstore> GetBookstores(string bookStoreName)
        {
            List<Bookstore> bookstoreList = _sqlService.GetBookstores(bookStoreName);
            return bookstoreList;
        }

        public List<Bookstore> ShowBookstores()
        {
            List<Bookstore> bookstoreList = _sqlService.ShowBookstores();
            return bookstoreList;
        }

        public void AddBookstore(string name)
        {
            _sqlService.AddBookstore(name);
        }
    }
}
