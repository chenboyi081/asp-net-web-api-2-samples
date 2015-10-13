using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Starring { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; }
        public string Story { get; set; }
    }
}