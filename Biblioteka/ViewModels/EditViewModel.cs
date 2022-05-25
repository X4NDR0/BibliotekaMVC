using Biblioteka.Facades.SQL.Models;
namespace Biblioteka.ViewModels
{
    public class EditViewModel
    {
        public Book Book { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
