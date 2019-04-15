using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels.PEDepartmentReportModels
{
    public class PEDepartmentReportModel<TEntity> where TEntity : class
    {
        [Required]
        public string DepartmentName { get; set; }

        public IReadOnlyCollection<PEDepartmentReportPhysicalEducationGroups<TEntity>> PhysicalEducationGroups { get; set; }
    }
}
