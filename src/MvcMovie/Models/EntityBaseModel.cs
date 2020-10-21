using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MvcMovie.Models
{
    public class EntityBaseModel
    {
        [AllowNull]
        public int Id { get; set; }
    }
}