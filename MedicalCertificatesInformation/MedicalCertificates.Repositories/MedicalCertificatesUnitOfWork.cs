using MedicalCertificates.Repositories.Interfaces;
using MedicalSCertificates.Repositories;

namespace MedicalCertificates.Repositories
{
    public class MedicalCertificatesUnitOfWork : UnitOfWork<MedicalCertificatesDbContext>, IMedicalCertificatesUnitOfWork
    {
        public MedicalCertificatesUnitOfWork(MedicalCertificatesDbContext context) : base(context)
        {
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {

        }
    }
}
