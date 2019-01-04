using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class PhysicalEducation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MedicalCertificate> MedicalCertificates { get; set; }
    }
}