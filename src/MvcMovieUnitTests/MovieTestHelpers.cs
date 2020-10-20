using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MvcMovie.Controllers;
using MvcMovie.Models;
using Newtonsoft.Json;

namespace MvcMovieUnitTests
{
    class Helpers
    {
        #region Private Properties
        private MoviesController _MoviesController;
        private List<Movie> _AllMovies;
        private List<Movie> _TestMovies;
        private readonly MvcMovieContext _context;
        #endregion
        #region Public Properties
        public List<Movie> AllMovies
        {
            get { return _AllMovies; }
            set { _AllMovies = value; }
        }
        public List<Movie> TestMovies
        {
            get { return _TestMovies; }
            set { _TestMovies = value; }
        }
        #endregion
        #region Constructor
        public MovieTestHelpers()
        {
            GetAllMovies();
        }
        #endregion

        public async void AddMovie(Movie movie)
        {
            await _MoviesController.Create(movie);
        }
        public async void DeleteAllMovies()
        {
            foreach (Movie m in AllMovies)
            {
                await _MoviesController.Delete(m.Id);
            }
        }
        public void GetAllMovies()
        {
            AllMovies = (from m in _context.Movie orderby m.ReleaseDate select m).ToList();
        }

        public void GetTestMoviesFromJson()
        {
            using StreamReader file = (StreamReader)GetInputFile("TestMovies.json");
            JsonSerializer serializer = new JsonSerializer();
            TestMovies = (List<Movie>)serializer.Deserialize(file, typeof(List<Movie>));
        }

        public static TextReader GetInputFile(string filename)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            string path = "LandscapeTests";
            return new StreamReader(thisAssembly.GetManifestResourceStream(path + "." + filename));
        }
    }
}
