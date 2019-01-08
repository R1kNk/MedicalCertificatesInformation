using System;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Business
{
    interface IReportService<TEntity, TContainer> where TEntity : class where TContainer : class
    {
        Task<TEntity> GetAllFromAsync(TContainer container);

        Task<TEntity> GetValidFromAsync(TContainer container);

        Task<TEntity> GetInvalidFromAsync(TContainer container);

        Task<TEntity> GetInvalidOnDateFromAsync(TContainer container, DateTime dateTime);

        Task<TEntity> GetValidOnDateFromAsync(TContainer container, DateTime dateTime);

        Task<TEntity> GetInvalidOnDateIntervalFromAsync(TContainer container, DateTime dateTime);

        Task<TEntity> GetValidOnDateIntervalFromAsync(TContainer container, DateTime dateTime);
    }
}
