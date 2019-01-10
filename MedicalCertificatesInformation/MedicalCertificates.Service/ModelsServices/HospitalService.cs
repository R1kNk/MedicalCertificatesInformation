using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    class HospitalService : CRUDService<Hospital>, IHospitalService
    {
        public HospitalService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
