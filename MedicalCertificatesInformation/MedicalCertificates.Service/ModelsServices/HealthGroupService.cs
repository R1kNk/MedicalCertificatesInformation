using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.Models
{
    public class HealthGroupService : CRUDService<HealthGroup>, IHealthGroupService
    {
        public HealthGroupService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
