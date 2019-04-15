using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class ConfigurePEDepartmentReport
    {
        public IReadOnlyList<int> DepartmentsId { get; set; }
    }
}
