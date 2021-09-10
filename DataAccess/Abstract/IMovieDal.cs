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
    public interface IMovieDal: IRepositoryBase<Movie>
    {
        Task<List<Movie>> getMoviesWithPagination(PaginationFilter paginationFilter);
        Task<Movie> GetRandomMovie();
    }
}
