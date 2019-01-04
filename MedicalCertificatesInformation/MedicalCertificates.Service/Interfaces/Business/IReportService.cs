using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Business
{
    interface IReportService<TEntity, TContainer> where TEntity : class where TContainer : class
    {
        Task<IReadOnlyCollection<TEntity>> GetAllFrom(TContainer container);

        Task<IReadOnlyCollection<TEntity>> GetValidFrom(TContainer container);

        Task<IReadOnlyCollection<TEntity>> GetInvalidFrom(TContainer container);

        Task<IReadOnlyCollection<TEntity>> GetInvalidOnDateFrom(TContainer container, DateTime dateTime);

        Task<IReadOnlyCollection<TEntity>> GetValidOnDateFrom(TContainer container, DateTime dateTime);

        Task<IReadOnlyCollection<TEntity>> GetInvalidOnDateIntervalFrom(TContainer container, DateTime dateTime);

        Task<IReadOnlyCollection<TEntity>> GetValidOnDateIntervalFrom(TContainer container, DateTime dateTime);
    }
}
