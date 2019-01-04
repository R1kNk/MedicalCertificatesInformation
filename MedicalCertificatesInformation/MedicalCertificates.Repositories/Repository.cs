using MedicalCertificates.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCertificates.Repositories
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

         readonly IDbContext Context;

         readonly DbSet<TEntity> dbSet;

        public Repository(IDbContext context)
        {
            Context = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
           return dbSet.Add(entity).Entity;
        }

        public void Delete(TEntity entity)
        {
            if (!dbSet.Local.Contains(entity))
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {            
            return await dbSet.ToListAsync();
        }

        public async Task<IReadOnlyCollection<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
           return await dbSet.Where(filterExpression).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await dbSet.SingleOrDefaultAsync(filterExpression);
        }

        public void Update(TEntity entity)
        {
            if (!dbSet.Local.Contains(entity))
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public IOrderedQueryable<TEntity> OrderBy<TOrderBy>(IQueryable<TEntity> entities, Expression<Func<TEntity, TOrderBy>> orderBy, bool ascending)
        {
            if (ascending)
            {
                var result = entities.OrderBy(orderBy);
                return result;
            }
            else
            {
                var result = entities.OrderByDescending(orderBy);
                return result;
            }
        }

        public IOrderedQueryable<TEntity> ThenOrderBy<TOrderBy>(IOrderedQueryable<TEntity> entities, Expression<Func<TEntity, TOrderBy>> thenOrderBy, bool ascending)
        {
            if (ascending)
            {
                var result = entities.ThenBy(thenOrderBy);
                return result;
            }
            else
            {
                var result = entities.OrderByDescending(thenOrderBy);
                return result;
            }
        }

        public IQueryable<TEntity> GetIQueryable()
        {
            return dbSet;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(IQueryable<TEntity> entities)
        {
            return await entities.ToListAsync();
        }

        public IQueryable<TEntity> FilterAsync(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> filterExpression)
        {
            return entities.Where(filterExpression);
        }
    }
}
