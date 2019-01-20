using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class ConfigureCourseReportViewModel : DefaultReportModel
    {
        [Required]
        public IReadOnlyList<int> CoursesId { get; set; }

        public ConfigureCourseReportViewModel()
        {
            ReportSorts = ReportSortPair.GetDefaultReportSortPair();
            ReportTypes = ReportTypePair.GetDefaultReportTypePairs();
            ReportWhichPeopleChose = WhichPeopleChosePair.GetDefaultPeopleChosePairs();
        }
    }
}
