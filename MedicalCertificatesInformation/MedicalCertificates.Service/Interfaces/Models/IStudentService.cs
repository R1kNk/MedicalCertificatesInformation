using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    public interface IStudentService : ICRUDService<Student>
    {
        Task<OperationResult<BusinessLogicResultError>> AddMedicalSertificateAsync(Student student, MedicalCertificate certificate);

        MedicalCertificate GetLastCertificate(Student student);

        IReadOnlyList<Student> SortStudents(IReadOnlyList<Student> students, bool valid, DateTime dateTime);

    }
}
