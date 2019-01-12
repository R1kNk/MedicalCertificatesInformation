using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    public interface IMedicalCertificateService : ICRUDService<MedicalCertificate>
    {
        Task<OperationResult<BusinessLogicResultError>> AddMedicalCertificateAsync(MedicalCertificate certificate,  int studentId);

        Task<OperationResult<BusinessLogicResultError>> EditMedicalCertificateAsync(MedicalCertificate updateCertificate);

        Task<OperationResult<BusinessLogicResultError>> DeleteMedicalCertificateAsync(int deleteCertificateId);

        MedicalCertificate GetLastCertificate(Student student);

        MedicalCertificate GetPenultimateCertificate(Student student);
    }
}
