using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    public class CourseService : CRUDService<Course>, ICourseService
    {
        public CourseService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationResult<string>> AddGroupAsync(Course course, Group group)
        {
            var entity = await GetByIdAsync(course.Id);
            group.Course = entity;
            entity.Groups.Add(group);
            await _unitOfWork.SaveAsync();
            return OperationResult<string>.CreateSuccessfulResult();
        }
    }
}
