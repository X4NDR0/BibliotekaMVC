namespace Biblioteka.Facades.SQL.Models
{
    public class Bookstore
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books = new List<Book>();
    }
}
