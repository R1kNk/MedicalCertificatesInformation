using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.Models
{
    public class StudentService : CRUDService<Student>, IStudentService
    {
        public StudentService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddMedicalSertificateAsync(Student student, MedicalCertificate certificate)
        {
            if (certificate.StartDate > certificate.FinishDate || certificate.CertificateTerm != certificate.FinishDate - certificate.StartDate)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>(){ BusinessLogicResultError.InvalidDate});
            var entity = await _tEntityRepository.GetByIdAsync(student.Id);
            var lastCertificate = entity.MedicalCertificates.LastOrDefault();
            if (lastCertificate != null)
            {
                if (lastCertificate.FinishDate > certificate.StartDate)
                    return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.OverlappingDate });
            }
            certificate.Student = entity;
            entity.MedicalCertificates.Add(certificate);
            await _unitOfWork.SaveAsync();
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }
    }
}
