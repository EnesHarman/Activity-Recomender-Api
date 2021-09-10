using Core.Entity.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class WatchedMovieDto : IDto
    {
        public int WatchedMovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
