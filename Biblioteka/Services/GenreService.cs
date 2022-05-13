﻿using Biblioteka.Models;

namespace Biblioteka.Services
{
    public class GenreService
    {
        private SQLService _sqlService = new SQLService();
        public Genre FindGenre(string genreName)
        {
            List<Genre> genreList = _sqlService.GetAllGenres();
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