using System.Collections.Generic;

namespace MedicalSertificates.DomainModel.Models
{
    public class HealthGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MedicalCertificate> MedicalCertificates { get; set; }
    }
}