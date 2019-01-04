using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.Models
{
    class GroupService : CRUDService<Group>, IGroupService
    {
        public GroupService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationResult<string>> AddStudentAsync(Group group, Student student)
        {
            var entity = await GetByIdAsync(group.Id);
            student.Group = entity;
            entity.Students.Add(student);
            await _unitOfWork.SaveAsync();
            return OperationResult<string>.CreateSuccessfulResult();
        }
    }
}
