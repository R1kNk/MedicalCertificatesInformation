using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Common;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    public interface ICourseService : ICRUDService<Course>
    {
        Task<OperationResult<string>> AddGroupAsync(Course course, Group group);
    }
}
