using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ReportModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalCertificates.Service.ReportModels
{
    public class GroupOfStudentsReport
    {
        public int All { get; private set; }
        public DateTime OnDate { get; private set; }
        public CertificatePresenceStat<StudentReport> CertificatePresenceStatistics { get; private set; }
        public HealthGroupStat<StudentReport> HealthGroupStat { get; private set; }
        public PhysicalEducationStat<StudentReport> PhysicalEducationStat { get; private set; }
        public List<string> FromWhatContainers { get; set; }
        
        public GroupOfStudentsReport()
        {

        }
        public GroupOfStudentsReport(IReadOnlyList<Student> students, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations, DateTime datetime)
        {
           
            IReadOnlyList<StudentReport> studentReports = GenerateStudentReports(students, datetime);
            if (studentReports == null)
                All = default(int);
            else All = studentReports.Count;
            OnDate = datetime;
            CertificatePresenceStatistics = GetCertificatePresenceStat(studentReports);

            HealthGroupStat = GetHealthGroupStat(studentReports, healthGroups);
            HealthGroupStat.HealthGroupStatistics = HealthGroupStat.HealthGroupStatistics.OrderByDescending(p => p.Count).ToList();

            PhysicalEducationStat = GetPhysicalEducationStat(studentReports, physicalEducations);
            PhysicalEducationStat.PhysicalEducationStatistics = PhysicalEducationStat.PhysicalEducationStatistics.OrderByDescending(p => p.Count).ToList();
            FromWhatContainers = new List<string>();
        }

        private IReadOnlyList<StudentReport> GenerateStudentReports(IReadOnlyList<Student> students, DateTime dateTime)
        {
            List<StudentReport> reports = new List<StudentReport>();
            foreach (var student in students)
            {
                if (student != null)
                {
                    var lastCertificate = student.MedicalCertificates.LastOrDefault();
                    StudentReport report = new StudentReport(student, lastCertificate, dateTime);
                    reports.Add(report);
                }
            }
            return reports;
        }

        public void SortByGroup()
        {
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.OrderBy(p => p.Group).ToList();
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderBy(p => p.Group).ToList();
            for(int i = HealthGroupStat.HealthGroupStatistics.Count-1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderBy(p => p.Group).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderBy(p => p.Group).ToList();
            }
        }

        public void SortByDepartment()
        {
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.OrderBy(p => p.Department).ToList();
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderBy(p => p.Department).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderBy(p => p.Department).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderBy(p => p.Department).ToList();
            }
        }

        public void SortByCourseAscending()
        {
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.OrderBy(p => p.Course).ToList();
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderBy(p => p.Course).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderBy(p => p.Course).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderBy(p => p.Course).ToList();
            }
        }

        public void SortByCourseDescending()
        {
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.OrderByDescending(p => p.Course).ToList();
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderByDescending(p => p.Course).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderByDescending(p => p.Course).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderByDescending(p => p.Course).ToList();
            }
        }

        public void SortByHealthGroup()
        {
            var list = CertificatePresenceStatistics.DontHave.EntityList.Where(p => p.HealthGroup == null).ToList();
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.Where(p => p.HealthGroup != null).OrderBy(p => p.HealthGroup.Name).ToList();
            CertificatePresenceStatistics.DontHave.EntityList.AddRange(list);
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderBy(p => p.HealthGroup.Name).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderBy(p => p.HealthGroup.Name).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderBy(p => p.HealthGroup.Name).ToList();
            }
        }

        public void SortByPhysicalEducation()
        {
            var list = CertificatePresenceStatistics.DontHave.EntityList.Where(p => p.PhysicalEducation == null).ToList();
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.Where(p => p.PhysicalEducation != null).OrderBy(p => p.PhysicalEducation.Name).ToList();
            CertificatePresenceStatistics.DontHave.EntityList.AddRange(list);
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderBy(p => p.PhysicalEducation.Name).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderBy(p => p.PhysicalEducation.Name).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderBy(p => p.PhysicalEducation.Name).ToList();
            }
        }

        public void SortBySurnameName()
        {
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.OrderBy(p => p.Surname).ThenBy(p=>p.Name).ToList();
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderBy(p => p.Surname).ThenBy(p => p.Name).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderBy(p => p.Surname).ThenBy(p => p.Name).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderBy(p => p.Surname).ThenBy(p => p.Name).ToList();
            }
        }

        public void SortByEndCertificateDateAscending()
        {
            var list = CertificatePresenceStatistics.DontHave.EntityList.Where(p => p.FinishDate == null).ToList(); ;
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.Where(p=>p.FinishDate!=null).OrderBy(p => p.FinishDate).ToList();
            CertificatePresenceStatistics.DontHave.EntityList.AddRange(list);
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderBy(p => p.FinishDate).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderBy(p => p.FinishDate).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderBy(p => p.FinishDate).ToList();
            }
        }

        public void SortByEndCertificateDateDescending()
        {
            var list = CertificatePresenceStatistics.DontHave.EntityList.Where(p => p.FinishDate == null).ToList();
            CertificatePresenceStatistics.DontHave.EntityList = CertificatePresenceStatistics.DontHave.EntityList.Where(p => p.FinishDate != null).OrderByDescending(p => p.FinishDate).ToList();
            CertificatePresenceStatistics.DontHave.EntityList.AddRange(list);
            CertificatePresenceStatistics.Have.EntityList = CertificatePresenceStatistics.Have.EntityList.OrderByDescending(p => p.FinishDate).ToList();
            for (int i = HealthGroupStat.HealthGroupStatistics.Count - 1; i >= 0; i--)
            {
                HealthGroupStat.HealthGroupStatistics[i].EntityList = HealthGroupStat.HealthGroupStatistics[i].EntityList.OrderByDescending(p => p.FinishDate).ToList();
            }
            for (int i = PhysicalEducationStat.PhysicalEducationStatistics.Count - 1; i >= 0; i--)
            {
                PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList = PhysicalEducationStat.PhysicalEducationStatistics[i].EntityList.OrderByDescending(p => p.FinishDate).ToList();
            }
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
