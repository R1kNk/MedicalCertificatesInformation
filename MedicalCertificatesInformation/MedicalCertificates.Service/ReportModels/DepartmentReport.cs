using MedicalCertificates.DomainModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace MedicalCertificates.Service.ReportModels
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
