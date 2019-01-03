using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSertificates.Service.Interfaces.Business
{
    interface IReportService<TEntity, TContainer> where TEntity : class where TContainer : class
    {
        Task<IReadOnlyCollection<TEntity>> GetAllFrom(TContainer container);

        Task<IReadOnlyCollection<TEntity>> GetAllValidFrom(TContainer container);

        Task<IReadOnlyCollection<TEntity>> GetAllInvalidFrom(TContainer container);

        Task<IReadOnlyCollection<TEntity>> GetAllInvalidOnDateFrom(TContainer container, DateTime dateTime);


    }
}
