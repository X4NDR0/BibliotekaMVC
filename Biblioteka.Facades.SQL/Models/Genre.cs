using System.Collections.Generic;

namespace Biblioteka.Facades.SQL.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Deleted { get; set; }
        public List<Book> Books { get; set; }
    }
}
