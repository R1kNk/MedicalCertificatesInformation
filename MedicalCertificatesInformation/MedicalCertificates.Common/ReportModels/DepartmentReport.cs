using MedicalCertificates.Common.ReportModels.Common;
using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalCertificates.Common.ReportModels
{
    public class DepartmentReport : GroupOfGroupsReport
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public DepartmentReport(Department department, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations)  : base(department.Courses.SelectMany(p => p.Groups).ToList(), healthGroups, physicalEducations)
        {
            Id = department.Id;
            Name = department.Name;
            
        }

    }
}
