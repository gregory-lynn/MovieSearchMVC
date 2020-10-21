//sing Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMovie.Models;
using System.Linq;
using System.Security.Cryptography;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MvcMovieUnitTests
{
    public class MovieUnitTests
    {
        private Helpers _helper = new Helpers();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Fact]
        public void DeleteAllMoviesTest()
        {
            try
            {
                _helper.DeleteAllMovies();
                _helper.GetAllMovies();

                Assert.Empty(_helper.AllMovies);
            }
            catch (Exception e)
            {
                var test = e.Message;
                // add logging code here
            }

        }

        [Fact]
        public void AddMoviesTest()
        {
            try
            {
                Assert.NotEmpty(_helper.AllMovies);
                _helper.AddMovies(_helper.TestMovies);
                Movie JsonMovie = (from m in _helper.TestMovies select m).FirstOrDefault();
                Movie TestMovie = (from m in _helper.AllMovies where m.Title.Equals(JsonMovie.Title) select m).FirstOrDefault();
                Assert.NotNull(TestMovie);
            }
            catch (Exception e)
            {
                var test = e.Message;
                // add logging code here
            }
        }
    }
}
