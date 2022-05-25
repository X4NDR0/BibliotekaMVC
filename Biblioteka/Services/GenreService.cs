using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Facades.SQL.Models;
using Biblioteka.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteka.Services
{
    public class GenreService : IGenreService
    {
        private ISqlFacade _sqlService;

        public GenreService(ISqlFacade sqlService)
        {
            _sqlService = sqlService;
        }

        public Genre FindGenre(string genreName)
        {
            List<Genre> genreList = GetAllGenres();
            Genre genre = genreList.Where(x => x.Name == genreName).FirstOrDefault();
            return genre;
        }
        public Genre EditGenre(int id)
        {
            List<Genre> genreList = _sqlService.GetAllGenres();
            Genre genre = genreList.Where(x => x.Id == id).FirstOrDefault();
            return genre;
        }

        public List<Genre> GetAllGenres()
        {
            return _sqlService.GetAllGenres();
        }

        public void EditGenre(Genre genre)
        {
            _sqlService.EditGenre(genre);
        }

        public void AddGenre(string newGenreName)
        {
            Genre genreAdd = new Genre { Name = newGenreName, Deleted = "false" };
            _sqlService.AddGenreToSql(genreAdd);
        }

        public void RemoveGenre(int id)
        {
            _sqlService.RemoveGenre(id);
        }

    }
}
