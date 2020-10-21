using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models.Entities
{
    public class Info : EntityBaseModel
    {
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public List<Directors> Directors { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public List<Genres> Genres { get; set; }
        public string Rank { get; set; }
        public string RunningTime { get; set; }
        public List<Actors> Actors { get; set; }
    }
}