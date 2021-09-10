using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Movie:IEntity
    {
        public int Id { get; set; }
        public string Poster_Link { get; set; }
        public string Series_Title { get; set; }
        public string Released_Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public decimal IMDB_Rating { get; set; }
        public string Overview { get; set; }
        public string Director { get; set; }
        public string Star1 { get; set; }
        public string Star2 { get; set; }
        public string Star3 { get; set; }
        public string Star4 { get; set; }
    }
}
