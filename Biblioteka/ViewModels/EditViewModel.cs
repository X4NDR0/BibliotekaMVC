using Biblioteka.Facades.SQL.Models;
using System.Collections.Generic;

namespace Biblioteka.ViewModels
{
    public class EditViewModel
    {
        public Book Book { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
