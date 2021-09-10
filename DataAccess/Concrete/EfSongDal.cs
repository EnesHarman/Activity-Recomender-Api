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
    public class EfSongDal : EfEntityRepositoryBase<Song, ActivityRcmDbContext>, ISongDal
    {
        public async Task<List<Song>> GetListWithPagination(PaginationFilter paginationFilter)
        {
            using(var context =  new ActivityRcmDbContext())
            {
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
                
                IQueryable<Song> query =  context.Set<Song>();
                return await query.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                     .Take(validFilter.PageSize).ToListAsync();

            }
        }

        public async Task<Song> GetRandomSong()
        {
            using(var context = new ActivityRcmDbContext())
            {
                var result = await context.Songs.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
                return result;
            }
        }
    }
}
