using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class MoviesEntityViewModel
    {
        public List<Entities.Movies> Movies;
        public string SearchString { get; set; }
    }
}