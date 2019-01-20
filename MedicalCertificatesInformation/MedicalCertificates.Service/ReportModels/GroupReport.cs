using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;

namespace MedicalCertificates.Service.ReportModels
{
    public class GroupReport : GroupOfStudentsReport
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        
        public GroupReport(Group group, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations, DateTime dateTime) : base(group.Students, healthGroups, physicalEducations, dateTime)
        {
            Id = group.Id;
            Name = group.Name;
        }

    }
}
