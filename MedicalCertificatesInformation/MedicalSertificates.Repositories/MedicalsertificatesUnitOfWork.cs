using MedicalSertificates.Repositories.Interfaces;

namespace MedicalSertificates.Repositories
{
    class MedicalSertificatesUnitOfWork : UnitOfWork<MedicalSertificatesDbContext>, IMedicalSertificatesUnitOfWork
    {
        public MedicalSertificatesUnitOfWork(MedicalSertificatesDbContext context) : base(context)
        {
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {

        }
    }
}
