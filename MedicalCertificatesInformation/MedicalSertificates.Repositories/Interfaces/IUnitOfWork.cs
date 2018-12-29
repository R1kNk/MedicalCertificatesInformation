using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSertificates.Repositories.Interfaces
{
    interface IUnitOfWork : IDisposable
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveAsync();
    }
}
