using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie;
using MvcMovie.Controllers;
using MvcMovie.Models;
using Newtonsoft.Json;
using Xunit;

namespace MvcMovieUnitTests
{
    class Helpers
    {
        #region Private Properties
        private List<Movie> _AllMovies;
        private List<Movie> _TestMovies;
        private MvcMovieContext _context;
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
        public Helpers()
        {
            Startup();
            GetAllMovies();
            GetTestMoviesFromJson();
        }
        #endregion

        public void AddMovie(Movie movie)
        {
            _context.Movie.Add(movie);
        }
        public void DeleteAllMovies()
        {
            foreach (Movie m in AllMovies)
            {
                _context.Movie.Remove(m);
            }
        }
        public void GetAllMovies()
        {
            AllMovies = (from m in _context.Movie orderby m.ReleaseDate select m).ToList();
        }

        public void GetTestMoviesFromJson()
        {
            try
            {
                using StreamReader file = (StreamReader)GetInputFile("TestMovies.json");
                JsonSerializer serializer = new JsonSerializer();
                TestMovies = (List<Movie>)serializer.Deserialize(file, typeof(List<Movie>));
            }
            catch (Exception e)
            {
                var test = e.Message;
            }
        }

        public static TextReader GetInputFile(string filename)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            string path = "MvcMovieUnitTests";
            return new StreamReader(thisAssembly.GetManifestResourceStream(path + "." + filename));
        }
        private void Startup()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.NotNull(webHost);
            Assert.NotNull(webHost.Services.GetRequiredService<MvcMovieContext>());
            _context = webHost.Services.GetService<MvcMovieContext>();
        }
    }
    public class Startup : MvcMovie.Startup
    {
        public Startup(IConfiguration config) : base(config) { }
    }
}
