using MedicalSertificates.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSertificates.Repositories
{
    class UnitOfWork<TContext> : IUnitOfWork where TContext : IDbContext
    {
        private IRepositoryProvider _repositoryProvider;
        private bool _disposed = false;

        protected IDbContext Context { get; private set; }

        public UnitOfWork(TContext context)
        {
            Context = context;
            _repositoryProvider = new RepositoryProvider<TContext>(context);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return _repositoryProvider.GetRepository<TEntity>();   
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }
                Context = null;
                _repositoryProvider = null;
                _disposed = true;
            }
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
