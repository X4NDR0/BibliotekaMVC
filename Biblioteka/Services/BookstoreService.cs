using Biblioteka.Models;

namespace Biblioteka.Services
{
    public class BookstoreService
    {
        private SQLService _sqlService = new SQLService();

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
