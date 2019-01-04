using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalCertificates.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Update(TEntity entity);

        void Delete(TEntity entity);

        TEntity Create(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByIdAsync(string id);

        Task<IReadOnlyCollection<TEntity>> GetAllAsync();

        Task<IReadOnlyCollection<TEntity>> GetAllAsync(IQueryable<TEntity> entities);

        Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<IReadOnlyCollection<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterExpression);

        IQueryable<TEntity> GetIQueryable();

        IQueryable<TEntity> FilterAsync(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> filterExpression);

        IOrderedQueryable<TEntity> OrderBy<TOrderBy>(IQueryable<TEntity> entities, Expression<Func<TEntity, TOrderBy>> orderBy, bool ascending);

        IOrderedQueryable<TEntity> ThenOrderBy<TOrderBy>(IOrderedQueryable<TEntity> entities, Expression<Func<TEntity, TOrderBy>> thenOrderBy, bool ascending);
    }
}
