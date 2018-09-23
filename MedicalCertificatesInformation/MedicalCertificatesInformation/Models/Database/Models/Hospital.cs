using System.Collections.Generic;

namespace MedicalCertificatesInformation.Models.Database.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }

        public List<MedicalCertificate> MedicalSertificates { get; set; }
    }
}