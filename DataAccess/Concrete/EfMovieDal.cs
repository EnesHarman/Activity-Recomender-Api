using Core.DataAccess.Concrete.EntityFramework;
using Core.Utilities.Pagtination;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EfMovieDal : EfEntityRepositoryBase<Movie, ActivityRcmDbContext>, IMovieDal
    {
        public async Task<List<Movie>> getMoviesWithPagination(PaginationFilter paginationFilter)
        {
            using(var context = new ActivityRcmDbContext())
            {
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                IQueryable<Movie> query = context.Set<Movie>();
                return await query.Skip(((int)validFilter.PageNumber - 1) * (int)validFilter.PageSize)
                                     .Take(validFilter.PageSize).ToListAsync();
            }
        }

        public async Task<Movie> GetRandomMovie()
        {
            using (var context = new ActivityRcmDbContext())
            {
                var result = await context.Movies.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
                return result;
            }
        }
    }
}
