using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    public class CourseService : CRUDService<Course>, ICourseService, IGetAllStudents<Course>
    {
        private readonly IRepository<Department> _departmentRepository;

        public CourseService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _departmentRepository = _unitOfWork.GetRepository<Department>();
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddCourseAsync(Course newCourse, int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DepartmentNotFound });

            var existingCourseInDepartment =  department.Courses.SingleOrDefault(p => p.Number == newCourse.Number);
            if (existingCourseInDepartment != null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DuplicateCourseNumber });

            newCourse.DepartmentId = department.Id;

            var result = await CreateAsync(newCourse);
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public IReadOnlyList<Student> GetAllStudents(Course course)
        {
            List<Student> studentList = new List<Student>();
            if (course == null)
                return studentList;

            if (course != null)
                studentList.AddRange(course.Groups.SelectMany(p=>p.Students).ToList());
            return studentList;
        }

    }
}
