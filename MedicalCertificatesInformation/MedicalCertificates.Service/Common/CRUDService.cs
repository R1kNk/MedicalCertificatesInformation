using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Common
{
    public class CRUDService<TEntity> : Interfaces.Common.CRUDService<TEntity> where TEntity : class
    {

        protected readonly IMedicalCertificatesUnitOfWork _unitOfWork;
        protected readonly IRepository<TEntity> _tEntityRepository;

        public CRUDService(IMedicalCertificatesUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tEntityRepository = _unitOfWork.GetRepository<TEntity>();

        }

        public TEntity Create(TEntity entity)
        {
            var result = _tEntityRepository.Create(entity);
            return result;
        }

        public OperationResult<string> Delete(TEntity entity)
        {
            _tEntityRepository.Delete(entity);
            return OperationResult<string>.CreateSuccessfulResult();
        }

        public async Task<IReadOnlyCollection<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterexpression)
        {
            var result = await _tEntityRepository.FilterAsync(filterexpression);
            return result;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            var result = await _tEntityRepository.GetAllAsync();
            return result;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var result = await _tEntityRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var result = await _tEntityRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            var result = await _tEntityRepository.GetSingleOrDefaultAsync(filterExpression);
            return result;
        }

        public OperationResult<string> Update(TEntity entity)
        {
            _tEntityRepository.Update(entity);
            return OperationResult<string>.CreateSuccessfulResult();
        }

    }
}
