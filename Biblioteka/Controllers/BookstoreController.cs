using Biblioteka.Services;
using Biblioteka.Facades.SQL.Models;
using Microsoft.AspNetCore.Mvc;
using Biblioteka.Interfaces;

namespace Biblioteka.Controllers
{
    public class BookstoreController : Controller
    {
        private IBookstore _bookstoreService;
        public BookstoreController(IBookstore IBookstore)
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
