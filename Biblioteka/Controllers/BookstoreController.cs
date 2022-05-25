using Biblioteka.Facades.SQL.Models;
using Biblioteka.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblioteka.Controllers
{
    public class BookstoreController : Controller
    {
        private IBookstoreService _bookstoreService;
        public BookstoreController(IBookstoreService IBookstore)
        {
            _bookstoreService = IBookstore;
        }

        [Route("")]
        [Route("Bookstore/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Bookstore/Index")]
        public IActionResult Index(string name)
        {
            _bookstoreService.AddBookstore(name);
            return View();
        }

        [Route("Bookstore/DisplayBookstores")]
        public IActionResult DisplayBookstores()
        {
            List<Bookstore> bookstoreList = _bookstoreService.ShowBookstores();
            return View(bookstoreList);
        }
    }
}
