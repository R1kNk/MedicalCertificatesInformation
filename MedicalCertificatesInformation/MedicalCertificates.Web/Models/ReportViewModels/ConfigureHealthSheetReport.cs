using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class ConfigureHealthSheetReport
    {
        public IReadOnlyList<int> GroupsId { get; set; }
    }
}
