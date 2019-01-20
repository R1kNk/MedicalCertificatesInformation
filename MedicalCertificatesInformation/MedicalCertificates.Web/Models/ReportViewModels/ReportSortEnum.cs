using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public enum ReportSortEnum
    {
        ByGroup = 1,
        ByDepartment,
        BySurnameName,
        ByEndDateAscending,
        ByEndDateDescending,
        ByCourseAscending,
        ByCourseDescending,
        ByHealthGroup,
        ByPhysicaleducationGroup
    }
}
