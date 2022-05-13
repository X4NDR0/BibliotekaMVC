using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class BookstoreController : Controller
    {
        private BookstoreService _bookstoreService = new BookstoreService();

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string name)
        {
            _bookstoreService.AddBookstore(name);
            return View();
        }

        public IActionResult DisplayBookstores()
        {
            return View(_bookstoreService.ShowBookstores());
        }
    }
}
