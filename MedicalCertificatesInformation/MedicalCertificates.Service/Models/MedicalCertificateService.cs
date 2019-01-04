using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.Common;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.Models
{
    class MedicalCertificateService : CRUDService<MedicalCertificate>, IMedicalCertificateService
    {
        public MedicalCertificateService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
