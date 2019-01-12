using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    public interface IGroupService : ICRUDService<Group>, IGetAllStudents<Group>
    {
        Task<OperationResult<BusinessLogicResultError>> AddGroupAsync(Group newGroup, int courseId);

    }
}
