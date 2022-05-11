using Biblioteka.Models;
namespace Biblioteka.ViewModels
{
    public class SortBooksByGenreViewModel
    {
        public List<Genre> GenreList { get; set; }
        public List<Book> Books { get; set; }
    }
}
