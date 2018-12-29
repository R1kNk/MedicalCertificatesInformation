using MedicalSertificates.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSertificates.Repositories
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

        public async Task<IReadOnlyCollection<TEntity>> GetAllASync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
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

    }
}
