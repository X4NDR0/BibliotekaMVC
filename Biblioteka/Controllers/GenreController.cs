using Biblioteka.Facades.SQL.Models;
using Biblioteka.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblioteka.Controllers
{
    public class GenreController : Controller
    {
        private IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Route("Genre/DisplayAllGenres")]
        public IActionResult DisplayAllGenres()
        {
            List<Genre> genreList = _genreService.GetAllGenres();
            return View(_genreService.GetAllGenres());
        }

        [Route("Genre/DisplayDeletedGenres")]
        public IActionResult DisplayDeletedGenres()
        {
            List<Genre> genreList = _genreService.GetAllGenres();
            return View(genreList);
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
            Genre genre = _genreService.EditGenre(id);
            return View(genre);
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
