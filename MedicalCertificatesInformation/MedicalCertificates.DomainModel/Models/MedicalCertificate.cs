using System;

namespace MedicalCertificates.DomainModel.Models
{
    public class MedicalCertificate
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public TimeSpan CertificateTerm { get; set; }
        public string GoogleDriveImageId { get; set; }


        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    
        public int HealthGroupId { get; set; }
        public virtual HealthGroup HealthGroup { get; set; }

        public int PhysicalEducationId { get; set; }
        public virtual PhysicalEducation PhysicalEducation { get; set; }

        public int HospitalId { get; set; }
        public virtual Hospital Hospital { get; set; }

    }
}