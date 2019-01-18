﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Repositories.Interfaces
{
    public interface IRepositoryProvider : IDisposable
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        void RegisterRepository<TEntity, TRepository>() where TEntity : class where TRepository : IRepository<TEntity>;

    }
}
