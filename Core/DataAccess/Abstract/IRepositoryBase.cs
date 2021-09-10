using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IRepositoryBase<TEntity>
        where TEntity : IEntity, new()
    {
        Task<List<TEntity>> Getlist(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity >Get(Expression<Func<TEntity, bool>> filter);
        Task<int> add(TEntity entity);
        Task update(TEntity entity);
        Task delete(TEntity entity);
    }
}
