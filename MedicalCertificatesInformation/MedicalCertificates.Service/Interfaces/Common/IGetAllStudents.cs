using MedicalCertificates.DomainModel.Models;
using System.Collections.Generic;

namespace MedicalCertificates.Service.Interfaces.Common
{
    public interface IGetAllStudents<TEntity> where TEntity : class
    {
        IReadOnlyList<Student> GetAllStudents(TEntity department);
    }
}
