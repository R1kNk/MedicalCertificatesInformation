using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalSertificates.Repositories.Interfaces
{
    interface IRepository<TEntity> where TEntity : class
    {
        void Update(TEntity entity);

        void Delete(TEntity entity);

        TEntity Create(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);

        Task<IReadOnlyCollection<TEntity>> GetAllASync();

        Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<IReadOnlyCollection<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterexpression);
    }
}
