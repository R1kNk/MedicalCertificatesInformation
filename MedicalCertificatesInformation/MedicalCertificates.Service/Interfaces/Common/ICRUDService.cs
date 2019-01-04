using MedicalCertificates.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Common
{
    interface CRUDService<TEntity> where TEntity : class
    {
        OperationResult<string> Update(TEntity entity);

        OperationResult<string> Delete(TEntity entity);

        TEntity Create(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByIdAsync(string id);

        Task<IReadOnlyCollection<TEntity>> GetAllAsync();

        Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<IReadOnlyCollection<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterexpression);


    }
}
