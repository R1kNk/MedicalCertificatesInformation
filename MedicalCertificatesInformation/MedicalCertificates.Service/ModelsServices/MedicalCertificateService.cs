using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    class MedicalCertificateService : CRUDService<MedicalCertificate>, IMedicalCertificateService
    {
        public MedicalCertificateService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
