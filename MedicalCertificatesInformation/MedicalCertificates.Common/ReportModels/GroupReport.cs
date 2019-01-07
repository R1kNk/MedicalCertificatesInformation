using MedicalCertificates.Common.ReportModels.Common;
using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalCertificates.Common.ReportModels
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
