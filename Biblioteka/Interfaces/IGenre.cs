using Biblioteka.Facades.SQL.Models;

namespace Biblioteka.Interfaces
{
    public interface IGenre
    {
        public Genre EditGenre(int id);
        public Genre FindGenre(string genreName);
        public List<Genre> GetAllGenres();
        public void EditGenre(Genre genre);
        public void AddGenre(string newGenreName);
        public void RemoveGenre(int id);
    }
}
