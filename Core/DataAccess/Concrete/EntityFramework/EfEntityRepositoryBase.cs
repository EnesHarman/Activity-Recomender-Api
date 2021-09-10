using Core.DataAccess.Abstract;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<int> add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();

                //return Convert.ToInt32(addedEntity.GetType().GetProperty("Id").GetValue(entity,null));
                return entity.Id;
            }
        }

        public async Task delete(TEntity entity)
        {
            using(var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                return await query.Where(filter).SingleOrDefaultAsync();
            } 
        }

        public async Task<List<TEntity>> Getlist(Expression<Func<TEntity, bool>> filter)
        {
            using(var context= new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if(filter == null)
                {
                    return await query.ToListAsync();
                }

                else
                {
                    return await query.Where(filter).ToListAsync();
                }
                
            }
        }

        public async Task update(TEntity entity)
        {
            using(var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
