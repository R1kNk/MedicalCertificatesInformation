using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Common;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    interface IGroupService : ICRUDService<Group>
    {
        Task<OperationResult<string>> AddStudentAsync(Group group, Student student);
    }
}
