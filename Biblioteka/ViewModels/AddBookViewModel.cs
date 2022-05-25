using Biblioteka.Facades.SQL.Models;
using System.Collections.Generic;

namespace Biblioteka.ViewModels
{
    public class AddBookViewModel
    {
        public List<Genre> Genres { get; set; }
        public List<Bookstore> BookStores { get; set; }
        public Book Book { get; set; }
    }
}
