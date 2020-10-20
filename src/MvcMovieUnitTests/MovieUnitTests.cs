//sing Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMovie.Models;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace MvcMovieUnitTests
{
    public class MovieUnitTests
    {
        private Helpers _helper = new Helpers();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Fact]
        public void DeleteMoviesTest()
        {
            _helper.DeleteAllMovies();
            _helper.GetAllMovies();

            Assert.Empty(_helper.AllMovies);
        }

        [Fact]
        public void AddMoviesTest()
        {
            try
            {
                _helper.GetTestMoviesFromJson();
                foreach (Movie m in _helper.TestMovies)
                {
                    _helper.AddMovie(m);
                }
            }
            catch
            {
                // add logging code here
            }
            Assert.NotEmpty(_helper.AllMovies);
            Movie testmovie = (from m in _helper.TestMovies select m).FirstOrDefault();
            Assert.Contains(testmovie, _helper.AllMovies);
        }
    }
}
