using Biblioteka.Facades.SQL.Models;
using System.Collections.Generic;

namespace Biblioteka.Interfaces
{
    public interface IGenreService
    {
        public Genre EditGenre(int id);
        public Genre FindGenre(string genreName);
        public List<Genre> GetAllGenres();
        public void EditGenre(Genre genre);
        public void AddGenre(string newGenreName);
        public void RemoveGenre(int id);
    }
}
