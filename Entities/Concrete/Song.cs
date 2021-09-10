using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Song :IEntity
    {
        public int Id { get; set; }
        public string song_name { get; set; }
        public string artist_name { get; set; }
        public string album_names { get; set; }
    }
}
