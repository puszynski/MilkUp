using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MilkUp.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> DeleteHard(TEntity entityToDelete);
        Task<bool> DeleteHard(int id);
        Task Delete(TEntity entityToDelete);
        Task Delete(int id);

        Task<TEntity> GetByID(int id);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                       string includeProperties = "");

        Task<IQueryable<TEntity>> GetQuery(Expression<Func<TEntity, bool>> filter = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                           string includeProperties = "");

        Task<TEntity> Insert(TEntity entity);   
        Task<TEntity> Update(TEntity entity);

        Task Save();
    }
}
