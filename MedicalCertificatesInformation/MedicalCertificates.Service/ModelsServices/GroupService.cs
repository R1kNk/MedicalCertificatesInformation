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
    class GroupService : CRUDService<Group>, IGroupService, IGetAllStudents<Group>
    {
        private readonly ICourseService _courseService;
        public GroupService(ICourseService courseService,IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _courseService = courseService;
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddGroupAsync(Group newGroup, int courseId)
        {
            var course = await _courseService.GetByIdAsync(courseId);
            if (course == null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.CourseNotFound });

            var existingGroup = await GetSingleOrDefaultAsync(p => p.Name == newGroup.Name);
            if(existingGroup != null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DuplicateGroupName });

            newGroup.CourseId = course.Id;
            var result = await CreateAsync(newGroup);
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public IReadOnlyList<Student> GetAllStudents(Group group)
        {
            List<Student> studentList = new List<Student>();
            if (group == null)
                return studentList;
            
                if (group != null)
                    studentList.AddRange(group.Students.ToList());
            return studentList;
        }
    }
}
