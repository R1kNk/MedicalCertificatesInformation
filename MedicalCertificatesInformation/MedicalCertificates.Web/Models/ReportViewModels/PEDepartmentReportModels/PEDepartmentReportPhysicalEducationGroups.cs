using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels.PEDepartmentReportModels
{
    public class PEDepartmentReportPhysicalEducationGroups<TEntity> where TEntity : class
    {
        public string PhysicalEducationName { get; set; }
        public IReadOnlyCollection<PEDepartmentReportPhusicalEducationGroupCourses<TEntity>> GroupedCourses { get; set; }
        
    }
}
