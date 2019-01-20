using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalCertificates.Service.ReportModels
{ 
    public class DepartmentReport : GroupOfGroupsReport
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public DepartmentReport(Department department, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations, DateTime dateTime)  : base(department.Courses.SelectMany(p => p.Groups).ToList(), healthGroups, physicalEducations, dateTime)
        {
            Id = department.Id;
            Name = department.Name;
            
        }

    }
}
