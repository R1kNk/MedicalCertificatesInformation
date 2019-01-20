using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class ConfigureDepartmentReportViewModel : DefaultReportModel
    {
        [Required]
        public IReadOnlyList<int> DepartmentsId { get; set; }

        public ConfigureDepartmentReportViewModel()
        {
            ReportSorts = ReportSortPair.GetDefaultReportSortPair();
            ReportTypes = ReportTypePair.GetDefaultReportTypePairs();
            ReportWhichPeopleChose = WhichPeopleChosePair.GetDefaultPeopleChosePairs();
        }
    }
}
