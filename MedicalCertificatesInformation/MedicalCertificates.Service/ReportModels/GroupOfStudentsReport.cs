using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ReportModels.Common;
using System.Collections.Generic;
using System.Linq;

namespace MedicalCertificates.Service.ReportModels
{
    public class GroupOfStudentsReport
    {
        public int All { get; private set; }
        public CertificatePresenceStat<StudentReport> CertificatePresenceStatistics { get; private set; }
        public HealthGroupStat<StudentReport> HealthGroupStat { get; private set; }
        public PhysicalEducationStat<StudentReport> PhysicalEducationStat { get; private set; }

        public GroupOfStudentsReport(IReadOnlyList<Student> students, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations)
        {
           
            IReadOnlyList<StudentReport> studentReports = GenerateStudentReports(students);
            if (studentReports == null)
                All = default(int);
            else All = studentReports.Count;
            CertificatePresenceStatistics = GetCertificatePresenceStat(studentReports);
            HealthGroupStat = GetHealthGroupStat(studentReports, healthGroups);
            PhysicalEducationStat = GetPhysicalEducationStat(studentReports, physicalEducations);
        }

        private IReadOnlyList<StudentReport> GenerateStudentReports(IReadOnlyList<Student> students)
        {
            List<StudentReport> reports = new List<StudentReport>();
            foreach (var student in students)
            {
                if (student != null)
                {
                    var lastCertificate = student.MedicalCertificates.LastOrDefault();
                    StudentReport report = new StudentReport(student, lastCertificate);
                    reports.Add(report);
                }
            }
            return reports;
        }



        private CertificatePresenceStat<StudentReport> GetCertificatePresenceStat(IReadOnlyList<StudentReport> studentReports)
        {
            if (studentReports == null)
                return new CertificatePresenceStat<StudentReport>(new List<StudentReport>(), new List<StudentReport>());

            List<StudentReport> haveList = studentReports.Where(p => p.CertificateValid == true).ToList();
            List<StudentReport> dontHaveList = studentReports.Where(p => p.CertificateValid == false).ToList();
            return new CertificatePresenceStat<StudentReport>(haveList, dontHaveList);
        }

        private HealthGroupStat<StudentReport> GetHealthGroupStat(IReadOnlyList<StudentReport> studentReports, IReadOnlyList<HealthGroup> healthGroups)
        {
            if (healthGroups == null)
            {
                return new HealthGroupStat<StudentReport>(new List<HealthGroup>(), new List<List<StudentReport>>());
            }
            var stats = new List<List<StudentReport>>();
            foreach (var healthGroup in healthGroups)
            {
                var list = studentReports.Where(p => p.HealthGroup != null && p.HealthGroup.Id == healthGroup.Id).ToList();
                stats.Add(list);
            }
            return new HealthGroupStat<StudentReport>(healthGroups, stats);
        }

        private PhysicalEducationStat<StudentReport> GetPhysicalEducationStat(IReadOnlyList<StudentReport> studentReports, IReadOnlyList<PhysicalEducation> physicalEducations)
        {
            if (physicalEducations == null)
            {
                return new PhysicalEducationStat<StudentReport>(new List<PhysicalEducation>(), new List<List<StudentReport>>());
            }
            var stats = new List<List<StudentReport>>();
            foreach (var physicalEducation in physicalEducations)
            {
                var list = studentReports.Where(p => p.PhysicalEducation != null && p.PhysicalEducation.Id == physicalEducation.Id).ToList();
                stats.Add(list);
            }
            return new PhysicalEducationStat<StudentReport>(physicalEducations, stats);
        }
    }
}
