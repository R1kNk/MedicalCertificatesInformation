using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Common;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    interface IDepartmentService : ICRUDService<Department>
    {
        Task<OperationResult<string>> AddCourseAsync(Department department, Course course);
    }
}

