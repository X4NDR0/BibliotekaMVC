using Biblioteka.Facades.SQL.Models;
using System.Collections.Generic;

namespace Biblioteka.ViewModels
{
    public class SortBooksByGenreViewModel
    {
        public List<Genre> GenreList { get; set; }
        public List<Book> Books { get; set; }
    }
}
