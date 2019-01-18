using MedicalCertificates.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Repositories
{
    public class RepositoryProvider<TContext> : IRepositoryProvider where TContext : IDbContext
    {
        private IDictionary<Type, Type> _entityTypeToRepositoryMapping;
        private IDictionary<Type, object> _repositories;
        private bool _disposed = false; 

        protected IDbContext Context { get; private set; }

        public RepositoryProvider(TContext context)
        {
            Context = context;
            _entityTypeToRepositoryMapping = new Dictionary<Type, Type>();
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)GetOrCreateRepository<TEntity>();
        }

        void IRepositoryProvider.RegisterRepository<TEntity, TRepository>()
        {
            RegisterRepository<TEntity, TRepository>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        private void RegisterRepository<TEntity, TRepository>() where TEntity : class 
        {
            _entityTypeToRepositoryMapping.Add(typeof(TEntity), typeof(TRepository));
        }

        private object GetOrCreateRepository<TEntity>() where TEntity : class
        {
            if(_repositories.TryGetValue(typeof(TEntity), out var cachedRepository))
            {
                return cachedRepository;
            }
            var repository = _entityTypeToRepositoryMapping.TryGetValue(typeof(TEntity), out var repositoryType)
                ? (Repository<TEntity>)Activator.CreateInstance(repositoryType, Context)
                : new Repository<TEntity>(Context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }
                Context = null;
                _disposed = true;
            }
        }

       
        
    }
}
