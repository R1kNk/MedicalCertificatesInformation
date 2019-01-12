using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Common;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    public interface ICourseService : ICRUDService<Course>
    {
        Task<OperationResult<BusinessLogicResultError>> AddCourseAsync(Course newCourse, int departmentId);
    }
}
