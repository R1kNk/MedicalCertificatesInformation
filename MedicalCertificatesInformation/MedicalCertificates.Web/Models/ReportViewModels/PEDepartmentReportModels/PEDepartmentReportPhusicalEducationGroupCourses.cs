using System.Collections.Generic;

namespace MedicalCertificates.Web.Models.ReportViewModels.PEDepartmentReportModels
{
    public class PEDepartmentReportPhusicalEducationGroupCourses<TEntity> where TEntity: class
    {
        public int CourseNumber { get; set; }
        public IReadOnlyCollection<TEntity> EntityList { get; set; }
    }
}
