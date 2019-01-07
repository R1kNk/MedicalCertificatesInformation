using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
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

        public async Task<OperationResult<string>> AddCourseAsync(Department department, Course course)
        {
            var entity = await GetByIdAsync(department.Id);
            course.Department = entity;
            entity.Courses.Add(course);
            await _unitOfWork.SaveAsync();
            return OperationResult<string>.CreateSuccessfulResult();
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
