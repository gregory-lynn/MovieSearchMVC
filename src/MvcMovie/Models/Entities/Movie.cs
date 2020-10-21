using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models.Entities
{
    public class Movies : EntityBaseModel
    {
        public string Year { get; set; }
        public string Title { get; set; }
        public Info Info { get; set; }
    }
}