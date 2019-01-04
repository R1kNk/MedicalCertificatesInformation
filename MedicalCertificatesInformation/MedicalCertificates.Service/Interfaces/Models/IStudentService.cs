using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    interface IStudentService : ICRUDService<Student>
    {
        Task<OperationResult<BusinessLogicResultError>> AddMedicalSertificateAsync(Student student, MedicalCertificate certificate);
    }
}
