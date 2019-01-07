using MedicalCertificates.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Common
{
    public interface ICRUDService<TEntity> where TEntity : class
    {
        Task<OperationResult<string>> UpdateAsync(TEntity entity);

        Task<OperationResult<string>> DeleteAsync(TEntity entity);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByIdAsync(string id);

        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<IReadOnlyList<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterexpression);


    }
}
