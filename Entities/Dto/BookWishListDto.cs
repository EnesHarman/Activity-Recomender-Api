using Core.Entity.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class BookWishListDto : IDto
    {
        public int WishBookId { get; set; }
        public Book Book { get; set; }
    }
}
