using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Facades.SQL.Models;
using System.Data.SqlClient;

namespace Biblioteka.Facades.SQL
{
    public class SqlFacade:ISqlData
    {
        private string _connectionString = "Data Source=DESKTOP-QS7CCGF\\SQLEXPRESS;Initial Catalog=Biblioteka;Integrated Security=true";

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

        public void EditBook(Book book, string genreName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                List<Genre> genreList = GetAllGenres();
                Genre genre = genreList.Where(x => x.Name == genreName).FirstOrDefault();
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
        }

        public void EditGenre(Genre genre)
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
        }

        public void RemoveGenre(int id)
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
        }

        public void RemoveBook(int id)
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
        }

        public int AddBookstore(string name)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                int id = 0;
                sqlConnection.Open();
                string command = "insert into dbo.Bookstore(bookStoreName) values (@bookStoreName) SELECT Scope_Identity()";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@bookStoreName", name);
                id = Convert.ToInt32(cmd.ExecuteScalar());
                return id;
            }
        }

        public int AddGenreToSql(Genre genre)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                int id = 0;
                sqlConnection.Open();
                string command = "insert into dbo.Genres(genreName,deleted) values (@genreName,@deleted) SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@genreName", genre.Name);
                cmd.Parameters.AddWithValue("@deleted", "false");
                id = Convert.ToInt32(cmd.ExecuteScalar());
                return id;
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
                        List<Genre> genreList = GetAllGenres();
                        Genre genreFind = genreList.Where(x => x.Name == genre).FirstOrDefault();
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
                        List<Genre> genreList = GetAllGenres();
                        Genre genreFind = genreList.Where(x => x.Name == genre).FirstOrDefault();
                        Book book = new Book { Id = id, Name = name, Price = price, Genre = genreFind, Deleted = deleted };
                        bookList.Add(book);
                    }
                }
                return bookList;
            }
        }

        public int AddBook(Book book, Bookstore bookstore)
        {
            int id = 0;
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "insert into dbo.Books(bookName,price,genre,deleted,bookStoreID) values (@bookName,@price,@genre,@deleted,@bookStoreID) SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@bookName", book.Name);
                cmd.Parameters.AddWithValue("@price", book.Price);
                cmd.Parameters.AddWithValue("@genre", book.Genre.Name);
                cmd.Parameters.AddWithValue("@deleted", "false");
                cmd.Parameters.AddWithValue("@bookStoreID", book.Bookstore.Id);
                
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            
            bookstore.Books.Add(book);
            UpdateBookstore(bookstore);
            return id;
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
    }
}
