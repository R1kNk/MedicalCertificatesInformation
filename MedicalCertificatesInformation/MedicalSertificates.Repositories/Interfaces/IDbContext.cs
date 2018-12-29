using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MedicalSertificates.Repositories.Interfaces
{
    interface IDbContext : IDisposable
    {
          DbSet<TEntity> Set<TEntity>() where TEntity : class;

          Task<int> SaveChangesAsync();
    }
}
