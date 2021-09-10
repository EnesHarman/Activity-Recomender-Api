using Core.DataAccess.Abstract;
using Core.Utilities.Pagtination;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBookDal : IRepositoryBase<Book>
    {
        Task<List<Book>> GetBooksWithPagination(PaginationFilter paginationFilter);
        Task<Book> GetRandomBook();
    }
}
