using Biblioteka.Interfaces;
using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Facades.SQL.Models;

namespace Biblioteka.Services
{
    public class GenreService:IGenre
    {
        private ISqlData _sqlService;

        public GenreService(ISqlData sqlService)
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
