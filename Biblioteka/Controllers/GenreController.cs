using Biblioteka.Interfaces;
using Biblioteka.Facades.SQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class GenreController : Controller
    {
        private IGenre _genreService;

        public GenreController(IGenre genreService)
        {
            _genreService = genreService;
        }

        [Route("Genre/DisplayAllGenres")]
        public IActionResult DisplayAllGenres()
        {
            return View(_genreService.GetAllGenres());
        }

        [Route("Genre/DisplayDeletedGenres")]
        public IActionResult DisplayDeletedGenres()
        {
            return View(_genreService.GetAllGenres());
        }

        [Route("Genre/AddGenre")]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        [Route("Genre/AddGenre")]
        public IActionResult AddGenre(string newGenreName)
        {
            _genreService.AddGenre(newGenreName);
            return RedirectToAction("DisplayAllGenres", "Genre");
        }

        [Route("Genre/EditGenre/{id}")]
        public IActionResult EditGenre(int id)
        {
            return View(_genreService.EditGenre(id));
        }

        [HttpPost]
        [Route("Genre/EditGenre/{id}")]
        public IActionResult EditGenre(Genre genre)
        {
            _genreService.EditGenre(genre);
            return RedirectToAction("DisplayAllGenres");
        }

        [Route("Genre/DisplayDeletedGenres/{id}")]
        public IActionResult RemoveGenre(int id)
        {
            _genreService.RemoveGenre(id);
            return RedirectToAction("DisplayDeletedGenres");
        }

    }
}
