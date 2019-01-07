using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    public interface IDepartmentService : ICRUDService<Department>, IGetAllStudents<Department>
    {
        Task<OperationResult<string>> AddCourseAsync(Department department, Course course);
    }
}

