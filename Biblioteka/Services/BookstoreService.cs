using Biblioteka.Facades.SQL.Models;
using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Interfaces;

namespace Biblioteka.Services
{
    public class BookstoreService:IBookstore
    {
        private ISqlData _sqlService;

        public BookstoreService(ISqlData sqlService)
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
            return _sqlService.GetBookstores(bookStoreName);
        }

        public List<Bookstore> ShowBookstores()
        {
            return _sqlService.ShowBookstores();
        }

        public void AddBookstore(string name)
        {
            _sqlService.AddBookstore(name);
        }
    }
}
