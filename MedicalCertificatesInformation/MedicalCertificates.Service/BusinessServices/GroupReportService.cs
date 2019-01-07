using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.BusinessServices
{
    public class GroupReportService : GroupOfStudentReportService<Group, IGroupService>
    {
        public GroupReportService(IGroupService entityService, IHealthGroupService healthGroupService, IPhysicalEducationService physicalEducationService, IStudentService studentService)
            : base(entityService, healthGroupService, physicalEducationService, studentService)
        {
        }
    }
}
