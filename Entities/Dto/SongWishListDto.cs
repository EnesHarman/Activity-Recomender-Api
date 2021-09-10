using Core.Entity.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class SongWishListDto:IDto
    {
        public int SongWishListId { get; set; }
        public Song Song { get; set; }
    }
}
