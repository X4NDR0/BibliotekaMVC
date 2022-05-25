using Biblioteka.Facades.SQL.Models;

namespace Biblioteka.Interfaces
{
    public interface IBookstore
    {
        public Bookstore FindBookstore(string bookStoreName);
        public List<Bookstore> GetBookstores(string bookStoreName);
        public List<Bookstore> ShowBookstores();
        public void AddBookstore(string name);
    }
}
