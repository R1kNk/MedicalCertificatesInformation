using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Models
{
    class CourseService : ICourseService
    {
        private readonly IMedicalCertificatesUnitOfWork _unitOfWork;
        private readonly IRepository<Course> _courseRepository;


        public CourseService(IMedicalCertificatesUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = _unitOfWork.GetRepository<Course>();

        }

        public Course Create(Course entity)
        {
            var result = _courseRepository.Create(entity);
            return result;
        }

        public OperationResult<string> Delete(Course entity)
        {
                _courseRepository.Delete(entity);
            return OperationResult<string>.CreateSuccessfulResult();
        }

        public async Task<IReadOnlyCollection<Course>> FilterAsync(Expression<Func<Course, bool>> filterexpression)
        {
            var result =  await _courseRepository.FilterAsync(filterexpression);
            return result;
        }

        public async Task<IReadOnlyCollection<Course>> GetAllAsync()
        {
            var result = await _courseRepository.GetAllAsync();
            return result;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            var result = await _courseRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<Course> GetByIdAsync(string id)
        {
            var result = await _courseRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<Course> GetSingleOrDefaultAsync(Expression<Func<Course, bool>> filterExpression)
        {
            var result = await _courseRepository.GetSingleOrDefaultAsync(filterExpression);
            return result;
        }

        public OperationResult<string> Update(Course entity)
        {
            _courseRepository.Update(entity);
            return OperationResult<string>.CreateSuccessfulResult();
        }
    }
}
