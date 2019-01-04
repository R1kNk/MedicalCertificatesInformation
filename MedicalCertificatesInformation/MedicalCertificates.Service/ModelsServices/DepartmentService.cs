using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.ModelsServices
{
    class DepartmentService : CRUDService<Department>, IDepartmentService
    {
        public DepartmentService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationResult<string>> AddCourseAsync(Department department, Course course)
        {
            var entity = await GetByIdAsync(department.Id);
            course.Department = entity;
            entity.Courses.Add(course);
            await _unitOfWork.SaveAsync();
            ApplicationUser applicationUser = new ApplicationUser();
            return OperationResult<string>.CreateSuccessfulResult();
        }
    }
}
