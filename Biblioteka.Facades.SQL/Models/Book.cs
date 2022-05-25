namespace Biblioteka.Facades.SQL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Genre Genre { get; set; }
        public Bookstore Bookstore { get; set; }
        public string Deleted { get; set; }
    }
}
