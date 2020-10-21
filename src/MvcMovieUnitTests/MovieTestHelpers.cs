using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            DeleteAllTestMovies();
        }
        #endregion

        public void AddMovie(Movie movie)
        {
            //List<Movie> tmpList = new List<Movie>();
            //tmpList.Add(movie);
            // AddMovies(tmpList);
            _context.Add(
                new Movie
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = movie.Genre,
                    Price = movie.Price
                });
            _context.SaveChanges();
        }
        public void AddMovies(List<Movie> movies)
        {
            //_context.Movie.AddRange(movies);
            //_context.SaveChanges();
            foreach (Movie m in movies)
            {
                AddMovie(m);
            }
        }
        public void DeleteAllMovies()
        {
            foreach (Movie m in AllMovies)
            {
                _context.Movie.Remove(m);
                _context.SaveChanges();
            }
        }
        public void DeleteAllTestMovies()
        {
            foreach (Movie m in TestMovies)
            {
                var tmpMovie = (from tmp in AllMovies where tmp.Title.Equals(m.Title) select tmp).FirstOrDefault();
                if (tmpMovie != null)
                {
                    _context.Movie.Remove(tmpMovie);
                    _context.SaveChanges();
                }
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
            var args = new string[0];
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.NotNull(webHost);
            Assert.NotNull(webHost.Services.GetRequiredService<MvcMovieContext>());
            _context = webHost.Services.GetService<MvcMovieContext>();
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MvcMovieContext>();
                context.Database.Migrate();
                SeedData.Initialize(services);
            }
            try
            {
                webHost.Start();
                _context.SaveChanges();
            }
            catch
            {
                // do nothing web host already started
            }
        }
    }
    public class Startup : MvcMovie.Startup
    {
        public Startup(IConfiguration config) : base(config) { }
    }
}
