using MedicalCertificates.DomainModel.Models;
using System;

namespace MedicalCertificates.Service.ReportModels
{
    public class StudentReport
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime? StartDate { get; private set;}
        public DateTime? FinishDate { get; private set; }
        public TimeSpan? CertificatePeriod { get; private set; }
        public bool CertificateValid { get; private set; }
        public PhysicalEducation PhysicalEducation { get; private set; }
        public HealthGroup HealthGroup  { get; private set; }


        public StudentReport(Student student, MedicalCertificate lastCertificate)
        {
            Id = student.Id;
            Name = student.Name;
            Surname = student.Surname;
            if (lastCertificate == null)
            {
                CertificateValid = false;
                return;
            }
            StartDate = lastCertificate.StartDate;
            FinishDate = lastCertificate.FinishDate;
            CertificatePeriod = lastCertificate.CertificateTerm;
            if (DateTime.Now > FinishDate)
                CertificateValid = false;
            else CertificateValid = true;
            PhysicalEducation = lastCertificate.PhysicalEducation;
            HealthGroup = lastCertificate.HealthGroup;
        }

    }
}
