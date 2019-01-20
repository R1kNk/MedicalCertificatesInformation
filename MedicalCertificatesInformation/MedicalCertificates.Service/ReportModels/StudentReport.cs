using MedicalCertificates.DomainModel.Models;
using System;

namespace MedicalCertificates.Service.ReportModels
{
    public class StudentReport
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Group { get; private set; }
        public int Course { get; private set; }
        public string Department { get; private set; }
        public DateTime? StartDate { get; private set;}
        public DateTime? FinishDate { get; private set; }
        public double CertificatePeriod { get; private set; }
        public int DaysBeforeEnd { get; set; }
        public bool CertificateValid { get; private set; }
        public PhysicalEducation PhysicalEducation { get; private set; }
        public HealthGroup HealthGroup  { get; private set; }


        public StudentReport(Student student, MedicalCertificate lastCertificate, DateTime dateTime)
        {
            Id = student.Id;
            Name = student.Name;
            Surname = student.Surname;
            Group = student.Group.Name;
            Course = student.Group.Course.Number;
            Department = student.Group.Course.Department.Name;
            if (lastCertificate == null)
            {
                CertificateValid = false;
                return;
            }
            StartDate = lastCertificate.StartDate;
            FinishDate = lastCertificate.FinishDate;
            CertificatePeriod = lastCertificate.CertificateTerm;
            if (dateTime > FinishDate)
                CertificateValid = false;
            else CertificateValid = true;
            if (CertificateValid)
            {
                DaysBeforeEnd = Convert.ToInt32((FinishDate - dateTime).Value.TotalDays);
            }
            PhysicalEducation = lastCertificate.PhysicalEducation;
            HealthGroup = lastCertificate.HealthGroup;
        }

    }
}
