using Biblioteka.Models;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class GenreController : Controller
    {
        private GenreService _genreService = new GenreService();
        public IActionResult DisplayAllGenres()
        {
            return View(_genreService.GetAllGenres());
        }

        public IActionResult DisplayDeletedGenres()
        {
            return View(_genreService.GetAllGenres());
        }

        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(string newGenreName)
        {
            _genreService.AddGenre(newGenreName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EditGenre(int id)
        {
            return View(_genreService.EditGenre(id));
        }

        [HttpPost]
        public IActionResult EditGenre(Genre genre)
        {
            _genreService.EditGenre(genre);
            return RedirectToAction("DisplayAllGenres");
        }

        public IActionResult RemoveGenre(int id)
        {
            _genreService.RemoveGenre(id);
            return RedirectToAction("DisplayDeletedGenres");
        }

    }
}
