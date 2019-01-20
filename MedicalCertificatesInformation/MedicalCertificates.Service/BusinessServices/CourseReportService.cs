using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.BusinessServices
{
    public class CourseReportService : GroupOfStudentReportService<Course, ICourseService>
    {
        public CourseReportService(ICourseService entityService, IHealthGroupService healthGroupService, IPhysicalEducationService physicalEducationService, IStudentService studentService) : base(entityService, healthGroupService, physicalEducationService, studentService)
        {
        }
    }
}