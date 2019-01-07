using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.BusinessServices
{
    public class DepartmentReportService : GroupOfStudentReportService<Department, IDepartmentService>
    {
        public DepartmentReportService(IDepartmentService entityService, IHealthGroupService healthGroupService, IPhysicalEducationService physicalEducationService, IStudentService studentService) : base(entityService, healthGroupService, physicalEducationService, studentService)
        {
        }
    }
}
