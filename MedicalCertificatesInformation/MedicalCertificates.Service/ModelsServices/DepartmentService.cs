using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.ModelsServices
{
    class DepartmentService : CRUDService<Department>, IDepartmentService
    {
        public DepartmentService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddDepartmentAsync(Department newDepartment)
        {
            var existingdepartment = await GetSingleOrDefaultAsync(p => p.Name == newDepartment.Name);
            if(existingdepartment!=null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DuplicateDepartmentName });

            var result = await CreateAsync(newDepartment);
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<BusinessLogicResultError>> EditDepartmentAsync(Department updateDepartment)
        {
            var department = await GetByIdAsync(updateDepartment.Id);
            if(department == null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DepartmentNotFound });

            var sameNameDepartment = await GetSingleOrDefaultAsync(p => p.Id != department.Id && p.Name == updateDepartment.Name);
            if (sameNameDepartment != null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DuplicateDepartmentName });

            department.Name = updateDepartment.Name;

            var result = await UpdateAsync(department);
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public IReadOnlyList<Student> GetAllStudents(Department department)
        {
            List<Student> studentList = new List<Student>();
            if (department == null)
                return studentList;
            foreach(var course in department.Courses)
            {
                if (course != null)
                    studentList.AddRange(course.Groups.SelectMany(p => p.Students).ToList());
            }
            return studentList;

        }
    }
}
