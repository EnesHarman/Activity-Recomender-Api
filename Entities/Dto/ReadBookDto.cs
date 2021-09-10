using Core.Entity.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ReadBookDto : IDto
    {
        public int ReadBookId { get; set; }
        public Book Book { get; set; }
    }
}
