using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    public interface IDepartmentService : ICRUDService<Department>, IGetAllStudents<Department>
    {
        Task<OperationResult<BusinessLogicResultError>> AddDepartmentAsync(Department newDepartment);

        Task<OperationResult<BusinessLogicResultError>> EditDepartmentAsync(Department updateDepartment);
    }
}

