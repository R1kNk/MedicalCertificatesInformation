using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.CommonServices
{
    public class CRUDService<TEntity> : ICRUDService<TEntity> where TEntity : class
    {

        protected readonly IMedicalCertificatesUnitOfWork _unitOfWork;
        protected readonly IRepository<TEntity> _tEntityRepository;

        public CRUDService(IMedicalCertificatesUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tEntityRepository = _unitOfWork.GetRepository<TEntity>();

        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = _tEntityRepository.Create(entity);
            await _unitOfWork.SaveAsync();
            return result;
        }

        public async Task<OperationResult<string>> DeleteAsync(TEntity entity)
        {
            //try
            //{
            _tEntityRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
            return OperationResult<string>.CreateSuccessfulResult();
            //}
            //catch (Exception ex)
            //{
            //    return OperationResult<string>.CreateUnsuccessfulResult(new List<string> { "Something went wrong. " + ex.Message });
            //}
        }

        public async Task<IReadOnlyList<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterexpression)
        {
            var result = await _tEntityRepository.FilterAsync(filterexpression);
            return result;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
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

        public async Task<OperationResult<string>> UpdateAsync(TEntity entity)
        {
            //try
            //{
                _tEntityRepository.Update(entity);
                await _unitOfWork.SaveAsync();
                return OperationResult<string>.CreateSuccessfulResult();
            //}
            //catch (Exception ex)
            //{
            //    return OperationResult<string>.CreateUnsuccessfulResult(new List<string> { "Something went wrong. " + ex.Message });
            //}
        }


    }
}
