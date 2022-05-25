using Biblioteka.Facades.SQL.Models;
using System.Collections.Generic;

namespace Biblioteka.Interfaces
{
    public interface IBookstoreService
    {
        public Bookstore FindBookstore(string bookStoreName);
        public List<Bookstore> GetBookstores(string bookStoreName);
        public List<Bookstore> ShowBookstores();
        public void AddBookstore(string name);
    }
}
