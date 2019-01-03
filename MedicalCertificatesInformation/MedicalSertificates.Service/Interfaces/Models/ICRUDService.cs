using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSertificates.Service.Interfaces.Models
{
    interface ICRUDService<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task Update(TEntity entity);


    }
}
