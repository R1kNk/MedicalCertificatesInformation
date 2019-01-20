using MedicalCertificates.Web.Models.ReportViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class ConfigureGroupReportViewModel : DefaultReportModel
    {
        public IReadOnlyList<int> GroupsId { get; set; }

        public ConfigureGroupReportViewModel()
        {
            ReportSorts = ReportSortPair.GetDefaultReportSortPair();
            ReportTypes = ReportTypePair.GetDefaultReportTypePairs();
            ReportWhichPeopleChose = WhichPeopleChosePair.GetDefaultPeopleChosePairs();
        }

    }
}
