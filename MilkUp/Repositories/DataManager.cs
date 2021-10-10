using Microsoft.EntityFrameworkCore;
using MilkUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MilkUp.Repositories
{
    public class DataManager<TEntity, TDataContext> : IRepository<TEntity>
        where TEntity : EntityBase
        where TDataContext : DbContext
    {
        protected readonly TDataContext _dataContext;
        internal DbSet<TEntity> _dbSet; 

        public DataManager(TDataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<TEntity>();
        }

        public async virtual Task<bool> DeleteHard(TEntity entityToDelete)
        {
            if (_dataContext.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
            return await _dataContext.SaveChangesAsync() > 1;
        }

        public async virtual Task<bool> DeleteHard(int id)
        {
            var entityToDelete = _dbSet.FindAsync(id);
            return await DeleteHard(entityToDelete.Result);
        }

        public async virtual Task Delete(TEntity entityToDelete)
        {
            entityToDelete.DateDeleted = DateTime.UtcNow;
            await Update(entityToDelete);
        }

        public async virtual Task Delete(int id)
        {
            var entityToDelete = _dbSet.FindAsync(id);
            entityToDelete.Result.DateDeleted = DateTime.UtcNow;
            await Update(entityToDelete.Result);
        }

        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, 
                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
                                               string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _dbSet.Where(x => !x.DateDeleted.HasValue);

                if (filter != null) 
                    query = query.Where(filter);

                foreach (var includedProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includedProperty);

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;   
                return null;
            }
        }

        public virtual async Task<TEntity> GetByID(int id)
        {
            var result = _dbSet.FindAsync(id);
            if (result != null && !result.Result.DateDeleted.HasValue)
                return await result;
            return null;
        }

        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            entity.DateAdded = DateTime.UtcNow;
            entity.DateUpdated = DateTime.UtcNow;

            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> Update(TEntity entityToUpdate)
        {
            entityToUpdate.DateUpdated = DateTime.UtcNow;

            var dbSet = _dataContext.Set<TEntity>();
            dbSet.Attach(entityToUpdate);
            _dataContext.Entry(entityToUpdate).State = EntityState.Modified;

            await Save();
            return entityToUpdate;
        }

        public virtual async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
