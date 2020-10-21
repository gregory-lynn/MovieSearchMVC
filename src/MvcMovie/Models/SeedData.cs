using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                //if (context.Movie.Any())
                //{
                //    return;   // DB has been seeded
                //}
                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M
                        },

                        new Movie
                        {
                            Title = "Ghostbusters ",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Price = 8.99M
                        },
                        new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Price = 3.99M
                        });

                }

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(
                    new Entities.Movies
                    {
                        Title = "When Harry Met Sally",
                        Year = "1989",
                        Info = GetInfo()
                    });
                    context.SaveChanges();
                }
            }
        }
        private static Info GetInfo()
        {
            var info = new Info
            {
                MovieId = 1,
                Directors = GetDirectors(),
                ReleaseDate = DateTime.Parse("2020-02-02"),
                Rating = 7.0m,
                Genres = GetGenres(),
                ImageUrl = "",
                Plot = "",
                Rank = "2.0",
                RunningTime = "9009",
                Actors = GetActors()
            };
            return info;
        }

        private static List<Directors> GetDirectors()
        {
            var directors = new List<Directors> {
                new Directors
                {
                    InfoId = 1,
                    Director = "Robert Redford"
                }
                // The rest of the categories here... add comma before new brackets
            };
            return directors;
        }
        private static List<Genres> GetGenres()
        {
            var genres = new List<Genres> {
                new Genres
                {
                    InfoId = 1,
                    Genre = "Action"
                }
                // The rest of the categories here... add comma before new brackets
            };
            return genres;
        }
        private static List<Actors> GetActors()
        {
            var actors = new List<Actors> {
                new Actors
                {
                    InfoId = 1,
                    Actor = "Robert Redford"
                }
                // The rest of the categories here... add comma before new brackets
            };
            return actors;
        }
    }
}