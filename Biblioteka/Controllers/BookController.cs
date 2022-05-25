using Biblioteka.Facades.SQL.Models;
using Biblioteka.Interfaces;
using Biblioteka.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblioteka.Controllers
{

    public class BookController : Controller
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("Book/AddBook")]
        public IActionResult AddBook(Book book, string genreName, string bookStoreName)
        {
            _bookService.AddBook(book, genreName, bookStoreName);
            return RedirectToAction("DisplayAllBooks", "Book");
        }

        [Route("Book/RemoveBook/{id}")]
        public IActionResult RemoveBook(int id)
        {
            _bookService.RemoveBook(id);
            return RedirectToAction("DisplayDeletedBooks", "Book");
        }

        [Route("Book/AddBook")]
        public IActionResult AddBook()
        {
            AddBookViewModel addBook = _bookService.AddBook();
            return View(addBook);
        }

        [Route("Book/DisplayAllBooks")]
        public IActionResult DisplayAllBooks()
        {
            List<Book> bookList = new List<Book>();
            return View(bookList);
        }

        [Route("Book/DisplayDeletedBooks")]
        public IActionResult DisplayDeletedBooks()
        {
            List<Book> bookList = new List<Book>();
            return View(bookList);
        }

        [Route("Book/EditBook/{id:int}")]
        public IActionResult EditBook(int id)
        {
            EditViewModel editBook = _bookService.EditBook(id);
            return View(editBook);
        }

        [HttpPost]
        [Route("Book/EditBook/{id:int}")]
        public IActionResult EditBook(Book book, string genreName)
        {
            _bookService.EditBook(book, genreName);
            return RedirectToAction("DisplayAllBooks", "Book");
        }

        [Route("Book/SortBooksByGenre")]
        public IActionResult SortBooksByGenre()
        {
            SortBooksByGenreViewModel sortBooksByGenreViewModel = _bookService.SortBooksByGenre();
            return View(sortBooksByGenreViewModel);
        }

        [HttpPost]
        [Route("Book/SortBooksByGenre")]
        public IActionResult SortBooksByGenre(string genreName)
        {
            SortBooksByGenreViewModel sortBooksByGenreViewModel = _bookService.SortBooksByGenre(genreName);
            return View(sortBooksByGenreViewModel);
        }

        [HttpPost]
        [Route("Book/DisplayAllBooks")]
        public IActionResult DisplayAllBooks(int sortOption)
        {
            List<Book> bookList = _bookService.DisplayAllBooks(sortOption);
            return View(bookList);
        }

    }
}
