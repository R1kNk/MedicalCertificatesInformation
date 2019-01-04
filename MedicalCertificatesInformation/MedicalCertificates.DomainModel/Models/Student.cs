using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GoogleDriveFolderId { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual List<MedicalCertificate> MedicalCertificates { get; set; }
    }
}