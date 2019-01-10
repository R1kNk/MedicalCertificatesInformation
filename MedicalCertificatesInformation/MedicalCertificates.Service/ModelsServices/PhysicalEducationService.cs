using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    class PhysicalEducationService : CRUDService<PhysicalEducation>, IPhysicalEducationService
    {
        public PhysicalEducationService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
