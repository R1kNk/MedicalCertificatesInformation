using MedicalCertificates.DomainModel.Models;
using System.Collections.Generic;

namespace MedicalCertificates.Service.ReportModels
{
    public class GroupReport : GroupOfStudentsReport
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        
        public GroupReport(Group group, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations) : base(group.Students, healthGroups, physicalEducations)
        {
            Id = group.Id;
            Name = group.Name;
        }

    }
}
