using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCertificates.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveAsync();
    }
}
