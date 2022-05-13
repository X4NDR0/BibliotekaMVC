using Biblioteka.Models;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService = new BookService();

        [HttpPost]
        public IActionResult AddBook(Book book, string genreName, string bookStoreName)
        {

            return RedirectToAction("DisplayAllBooks");
        }

        public IActionResult RemoveBook(int id)
        {
            _bookService.RemoveBook(id);
            return RedirectToAction("DisplayDeletedBooks");
        }

        public IActionResult AddBook()
        {
            return View(_bookService.AddBook());
        }

        public IActionResult DisplayAllBooks()
        {
            return View(_bookService.GetBooks());
        }

        public IActionResult DisplayDeletedBooks()
        {
            return View(_bookService.GetBooks());
        }

        public IActionResult EditBook(int id)
        {
            return View(_bookService.EditBook(id));
        }

        [HttpPost]
        public IActionResult EditBook(Book book, string genreName)
        {
            _bookService.EditBook(book, genreName);
            return RedirectToAction("DisplayAllBooks");
        }

        public IActionResult SortBooksByGenre()
        {

            return View(_bookService.SortBooksByGenre());
        }

        [HttpPost]
        public IActionResult SortBooksByGenre(string genreName)
        {
            return View(_bookService.SortBooksByGenre(genreName));
        }

        [HttpPost]
        public IActionResult DisplayAllBooks(int sortOption)
        {
            return View(_bookService.DisplayAllBooks(sortOption));
        }

    }
}
