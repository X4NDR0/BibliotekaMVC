using Biblioteka.Models;
using Biblioteka.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Biblioteka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private int _maxBookID;
        private string _connectionString = "Data Source=DESKTOP-QS7CCGF\\SQLEXPRESS;Initial Catalog=Biblioteka;Integrated Security=true";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            AddBookStoreToSQL(name);
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Book book, string genreName, string bookStoreName)
        {
            if (GetBooks().Count != 0)
            {
                _maxBookID = GetBooks().Max(x => x.Id) + 1;
            }
            else
            {
                _maxBookID = 1;
            }
            Genre genre = FindGenre(genreName);
            Bookstore bookstore = FindBookstore(bookStoreName);
            book.Bookstore = bookstore;
            book.Genre = genre;
            book.Deleted = "false";
            book.Id = _maxBookID;


            AddBookToSQL(book, bookstore);
            return RedirectToAction("DisplayAllBooks");
        }

        public void UpdateBookstore(Bookstore bookstore)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string bookIds = string.Empty;

                if (bookstore.Books != null)
                {
                    foreach (Book book in bookstore.Books)
                    {
                        bookIds += book.Id + ";";
                    }
                    bookIds = bookIds.Remove(bookIds.Length - 1);
                }
                else
                {
                    bookIds = GetBooks().Max(x => x.Id).ToString();
                }

                sqlConnection.Open();
                string command = "update dbo.Bookstore SET bookIds=@bookIds where id=@id";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@bookIds", bookIds);
                cmd.Parameters.AddWithValue("@id", bookstore.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IActionResult AddBook()
        {
            List<Genre> genreList = new List<Genre>();
            foreach (Genre genre in GetAllGenres())
            {
                if (genre.Deleted.Equals("false"))
                {
                    genreList.Add(genre);
                }
            }

            AddBookViewModel addBookViewModel = new AddBookViewModel { Genres = genreList, BookStores = ShowBookstores() };
            return View(addBookViewModel);
        }

        public IActionResult DisplayAllBooks()
        {
            return View(GetBooks());
        }

        public IActionResult DisplayDeletedBooks()
        {
            return View(GetBooks());
        }

        public IActionResult DisplayBookstores()
        {
            return View(ShowBookstores());
        }

        public IActionResult DisplayAllGenres()
        {
            return View(GetAllGenres());
        }

        public IActionResult DisplayDeletedGenres()
        {
            return View(GetAllGenres());
        }

        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(string newGenreName)
        {
            Genre genreAdd = new Genre { Name = newGenreName, Deleted = "false" };
            AddGenreToSql(genreAdd);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EditGenre(int id)
        {
            List<Genre> genreList = GetAllGenres();
            Genre genre = genreList.Where(x => x.Id == id).FirstOrDefault();
            return View(genre);
        }

        [HttpPost]
        public IActionResult EditGenre(Genre genre)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "update dbo.Genres set genreName=@genreName where id=@id";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@id", genre.Id);
                cmd.Parameters.AddWithValue("@genreName", genre.Name);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("DisplayAllGenres");
        }

        public IActionResult EditBook(int id)
        {
            List<Book> bookList = GetBooks();
            Book book = bookList.Where(x => x.Id == id).FirstOrDefault();

            EditViewModel model = new EditViewModel { Book = book, Genres = GetAllGenres() };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditBook(Book book, string genreName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                Genre genre = FindGenre(genreName);
                book.Genre = genre;
                sqlConnection.Open();
                string command = "update dbo.Books set bookName=@bookName,price=@price,genre=@genre,deleted=@deleted where id=@id";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@id", book.Id);
                cmd.Parameters.AddWithValue("@bookName", book.Name);
                cmd.Parameters.AddWithValue("@deleted", "false");
                cmd.Parameters.AddWithValue("@price", book.Price);
                cmd.Parameters.AddWithValue("@genre", book.Genre.Name);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("DisplayAllBooks");
        }

        public IActionResult RemoveGenre(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "update dbo.Genres set deleted=@deleted where id=@id";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@deleted", "true");
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("DisplayDeletedGenres");
        }

        public IActionResult RemoveBook(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "update dbo.Books set deleted=@deleted where id=@id";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@deleted", "true");
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("DisplayDeletedBooks");
        }

        [HttpPost]
        public IActionResult DisplayAllBooks(int sortOption)
        {
            List<Book> sortedList = new List<Book>();
            switch (sortOption)
            {
                case 1:
                    sortedList = SortBooksByNameAscending();
                    break;

                case 2:
                    sortedList = SortBooksByNameDescending();
                    break;

                case 3:
                    sortedList = SortBooksByPriceAscending();
                    break;

                case 4:
                    sortedList = SortBooksByPriceDescending();
                    break;

                default:
                    break;
            }
            return View(sortedList);
        }

        public IActionResult SortBooksByGenre()
        {
            SortBooksByGenreViewModel sortBooksByGenreViewModel = new SortBooksByGenreViewModel();
            sortBooksByGenreViewModel.GenreList = GetAllGenres();
            sortBooksByGenreViewModel.Books = GetBooks();
            return View(sortBooksByGenreViewModel);
        }


        [HttpPost]
        public IActionResult SortBooksByGenre(string genreName)
        {
            List<Book> bookList = GetBooks();
            List<Book> sortedList = bookList.Where(x => x.Genre.Name == genreName).ToList();

            SortBooksByGenreViewModel sortBooksByGenreViewModel = new SortBooksByGenreViewModel();
            sortBooksByGenreViewModel.GenreList = GetAllGenres();
            sortBooksByGenreViewModel.Books = sortedList;

            return View(sortBooksByGenreViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<Book> SortBooksByNameAscending()
        {
            List<Book> allBooks = GetBooks();
            List<Book> sorted = allBooks.OrderBy(x => x.Name).ToList();
            return sorted;
        }

        public List<Book> SortBooksByNameDescending()
        {
            List<Book> allBooks = GetBooks();
            List<Book> sorted = allBooks.OrderByDescending(x => x.Name).ToList();
            return sorted;
        }

        public List<Book> SortBooksByPriceAscending()
        {
            List<Book> allBooks = GetBooks();
            List<Book> sorted = allBooks.OrderBy(x => x.Price).ToList();
            return sorted;
        }

        public List<Book> SortBooksByPriceDescending()
        {
            List<Book> allBooks = GetBooks();
            List<Book> sorted = allBooks.OrderByDescending(x => x.Price).ToList();
            return sorted;
        }

        public Genre FindGenre(string genreName)
        {
            List<Genre> genreList = GetAllGenres();
            Genre genre = genreList.Where(x => x.Name == genreName).FirstOrDefault();
            return genre;
        }

        public List<Genre> GetAllGenres()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                List<Genre> genreList = new List<Genre>();
                sqlConnection.Open();
                string command = "select * from dbo.Genres";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string name = reader["genreName"].ToString();
                        string deleted = reader["deleted"].ToString();

                        Genre genre = new Genre { Id = id, Name = name, Deleted = deleted };
                        genreList.Add(genre);
                    }
                }
                return genreList;
            }
        }

        public void AddBookStoreToSQL(string name)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "insert into dbo.Bookstore(bookStoreName) values (@bookStoreName)";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@bookStoreName", name);
                cmd.ExecuteNonQuery();
            }
        }

        public Bookstore FindBookstore(string bookStoreName)
        {
            List<Bookstore> bookstores = GetBookstores(bookStoreName);
            Bookstore bookStore = bookstores[0];
            return bookStore;
        }

        public void AddBookToSQL(Book book, Bookstore bookstore)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "insert into dbo.Books(bookName,price,genre,deleted,bookStoreID) values (@bookName,@price,@genre,@deleted,@bookStoreID)";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@bookName", book.Name);
                cmd.Parameters.AddWithValue("@price", book.Price);
                cmd.Parameters.AddWithValue("@genre", book.Genre.Name);
                cmd.Parameters.AddWithValue("@deleted", "false");
                cmd.Parameters.AddWithValue("@bookStoreID", book.Bookstore.Id);
                cmd.ExecuteNonQuery();
            }
            bookstore.Books.Add(book);
            UpdateBookstore(bookstore);
        }

        public void AddGenreToSql(Genre genre)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "insert into dbo.Genres(genreName,deleted) values (@genreName,@deleted)";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@genreName", genre.Name);
                cmd.Parameters.AddWithValue("@deleted", "false");
                cmd.ExecuteNonQuery();
            }
        }

        public List<Book> GetBooks()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                List<Book> bookList = new List<Book>();
                sqlConnection.Open();
                string command = "select * from dbo.Books";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string name = reader["bookName"].ToString();
                        decimal price = decimal.Parse(reader["price"].ToString());
                        string genre = reader["genre"].ToString();
                        string deleted = reader["deleted"].ToString();
                        Book book = new Book { Id = id, Name = name, Price = price, Genre = FindGenre(genre), Deleted = deleted };
                        bookList.Add(book);
                    }
                }
                return bookList;
            }
        }

        public Book FindBook(int bookId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "SELECT * FROM Books WHERE ID=@id";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@id", bookId);
                List<Book> bookList = new List<Book>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Book book = null;
                    while (reader.Read())
                    {
                        int.TryParse(reader["id"].ToString(), out int id);
                        string bookName = reader["bookName"].ToString();
                        decimal.TryParse(reader["price"].ToString(), out decimal price);
                        string genre = reader["genre"].ToString();
                        Genre genreFind = FindGenre(genre);
                        string deleted = reader["deleted"].ToString();
                        book = new Book { Id = id, Name = bookName, Price = price, Genre = genreFind, Deleted = deleted };
                    }
                    return book;
                }
            }
        }

        public List<Bookstore> ShowBookstores()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                List<Bookstore> bookStoreList = new List<Bookstore>();
                sqlConnection.Open();
                string command = "select * from dbo.Bookstore";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string name = reader["bookStoreName"].ToString();
                        string bookIds = reader["bookIds"].ToString();

                        Bookstore bookStore = new Bookstore { Id = id, Name = name };
                        bookStoreList.Add(bookStore);
                    }
                }
                return bookStoreList;
            }
        }

        public List<Bookstore> GetBookstores(string bookStoreName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                List<Bookstore> bookStoreList = new List<Bookstore>();
                List<Book> bookList = new List<Book>();
                sqlConnection.Open();
                string command = "select * from dbo.Bookstore where bookStoreName=@bookStoreName";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@bookStoreName", bookStoreName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string name = reader["bookStoreName"].ToString();
                        string bookIds = reader["bookIds"].ToString();

                        if (!string.IsNullOrEmpty(bookIds))
                        {
                            string[] bookIdSplit = bookIds.Split(';');
                            foreach (string book in bookIdSplit)
                            {
                                Book bookLoad = FindBook(int.Parse(book));
                                bookList.Add(bookLoad);
                            }
                            Bookstore bookStore = new Bookstore { Id = id, Name = name, Books = bookList };
                            bookStoreList.Add(bookStore);
                        }
                        else
                        {
                            Bookstore bookStore = new Bookstore { Id = id, Name = name };
                            bookStoreList.Add(bookStore);
                        }
                    }
                }
                return bookStoreList;
            }
        }
    }
}