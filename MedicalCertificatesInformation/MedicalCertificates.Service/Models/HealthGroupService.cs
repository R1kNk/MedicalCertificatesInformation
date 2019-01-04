using MedicaCertificates.Service.Interfaces.Models;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.Common;

namespace MedicalCertificates.Service.Models
{
    public class HealthGroupService : CRUDService<HealthGroup>, IHealthGroupService
    {
        public HealthGroupService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
