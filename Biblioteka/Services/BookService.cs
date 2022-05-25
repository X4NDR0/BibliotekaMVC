using Biblioteka.Interfaces;
using Biblioteka.Facades.SQL.Models;
using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Models;
using Biblioteka.ViewModels;

namespace Biblioteka.Services
{
    public class BookService:IBook
    {
        private ISqlData _sqlService;
        private IGenre _genreService;
        private IBookstore _bookstoreService;
        private int _maxBookID;

        public BookService(ISqlData sqlService, IGenre genreService, IBookstore bookstoreService)
        {
            _sqlService = sqlService;
            _genreService = genreService;
            _bookstoreService = bookstoreService;
        }

        public List<Book> SortBooksByNameAscending()
        {
            List<Book> allBooks = _sqlService.GetBooks();
            List<Book> sorted = allBooks.OrderBy(x => x.Name).ToList();
            return sorted;
        }

        public List<Book> SortBooksByNameDescending()
        {
            List<Book> allBooks = _sqlService.GetBooks();
            List<Book> sorted = allBooks.OrderByDescending(x => x.Name).ToList();
            return sorted;
        }

        public List<Book> SortBooksByPriceAscending()
        {
            List<Book> allBooks = _sqlService.GetBooks();
            List<Book> sorted = allBooks.OrderBy(x => x.Price).ToList();
            return sorted;
        }

        public List<Book> SortBooksByPriceDescending()
        {
            List<Book> allBooks = _sqlService.GetBooks();
            List<Book> sorted = allBooks.OrderByDescending(x => x.Price).ToList();
            return sorted;
        }

        public List<Book> DisplayAllBooks(int sortOption)
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
            return sortedList;
        }

        public void EditBook(Book book, string genreName)
        {
            _sqlService.EditBook(book, genreName);
        }

        public EditViewModel EditBook(int id)
        {
            List<Book> bookList = GetBooks();
            Book book = bookList.Where(x => x.Id == id).FirstOrDefault();

            EditViewModel model = new EditViewModel { Book = book, Genres = _genreService.GetAllGenres() };

            return model;
        }

        public List<Book> GetBooks()
        {
            return _sqlService.GetBooks();
        }

        public AddBookViewModel AddBook()
        {
            List<Genre> genreList = new List<Genre>();
            foreach (Genre genre in _genreService.GetAllGenres())
            {
                if (genre.Deleted.Equals("false"))
                {
                    genreList.Add(genre);
                }
            }

            AddBookViewModel addBookViewModel = new AddBookViewModel { Genres = genreList, BookStores = _bookstoreService.ShowBookstores() };

            return addBookViewModel;
        }

        public void RemoveBook(int id)
        {
            _sqlService.RemoveBook(id);
        }

        public void AddBook(Book book, string genreName, string bookStoreName)
        {
            if (GetBooks().Count != 0)
            {
                _maxBookID = GetBooks().Max(x => x.Id) + 1;
            }
            else
            {
                _maxBookID = 1;
            }
            Genre genre = _genreService.FindGenre(genreName);
            Bookstore bookstore = _bookstoreService.FindBookstore(bookStoreName);
            book.Bookstore = bookstore;
            book.Genre = genre;
            book.Deleted = "false";
            book.Id = _maxBookID;

            _sqlService.AddBook(book, bookstore);
        }

        public SortBooksByGenreViewModel SortBooksByGenre()
        {
            SortBooksByGenreViewModel sortBooksByGenreViewModel = new SortBooksByGenreViewModel();
            sortBooksByGenreViewModel.GenreList = _genreService.GetAllGenres();
            sortBooksByGenreViewModel.Books = GetBooks();
            return sortBooksByGenreViewModel;
        }

        public SortBooksByGenreViewModel SortBooksByGenre(string genreName)
        {
            List<Book> bookList = GetBooks();
            List<Book> sortedList = bookList.Where(x => x.Genre.Name == genreName).ToList();

            SortBooksByGenreViewModel sortBooksByGenreViewModel = new SortBooksByGenreViewModel();
            sortBooksByGenreViewModel.GenreList = _genreService.GetAllGenres();
            sortBooksByGenreViewModel.Books = sortedList;
            return sortBooksByGenreViewModel;
        }

    }
}
