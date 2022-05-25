using Biblioteka.Interfaces;
using Biblioteka.Facades.SQL.Models;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{

    public class BookController : Controller
    {
        private IBook _bookService;

        public BookController(IBook bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("Book/AddBook")]
        public IActionResult AddBook(Book book, string genreName, string bookStoreName)
        {
            _bookService.AddBook(book, genreName, bookStoreName);
            return RedirectToAction("DisplayAllBooks","Book");
        }

        [Route("Book/RemoveBook/{id}")]
        public IActionResult RemoveBook(int id)
        {
            _bookService.RemoveBook(id);
            return RedirectToAction("DisplayDeletedBooks","Book");
        }

        [Route("Book/AddBook")]
        public IActionResult AddBook()
        {
            return View(_bookService.AddBook());
        }

        [Route("Book/DisplayAllBooks")]
        public IActionResult DisplayAllBooks()
        {
            return View(_bookService.GetBooks());
        }

        [Route("Book/DisplayDeletedBooks")]
        public IActionResult DisplayDeletedBooks()
        {
            return View(_bookService.GetBooks());
        }

        [Route("Book/EditBook/{id:int}")]
        public IActionResult EditBook(int id)
        {
            return View(_bookService.EditBook(id));
        }

        [HttpPost]
        [Route("Book/EditBook/{id:int}")]
        public IActionResult EditBook(Book book, string genreName)
        {
            _bookService.EditBook(book, genreName);
            return RedirectToAction("DisplayAllBooks","Book");
        }

        [Route("Book/SortBooksByGenre")]
        public IActionResult SortBooksByGenre()
        {
            return View(_bookService.SortBooksByGenre());
        }

        [HttpPost]
        [Route("Book/SortBooksByGenre")]
        public IActionResult SortBooksByGenre(string genreName)
        {
            return View(_bookService.SortBooksByGenre(genreName));
        }

        [HttpPost]
        [Route("Book/DisplayAllBooks")]
        public IActionResult DisplayAllBooks(int sortOption)
        {
            return View(_bookService.DisplayAllBooks(sortOption));
        }

    }
}
