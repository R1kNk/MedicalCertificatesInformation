using MedicalCertificates.Common.ReportModels.Common;
using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalCertificates.Common.ReportModels
{
    class GroupOfDepartmentsReport
    {
        public int All { get; set; }
       
        public CertificatePresenceStat<DepartmentReport> CertificatePresenceStatistics { get; set; }
        public HealthGroupStat<DepartmentReport> HealthGroupStat { get; set; }
        public PhysicalEducationStat<DepartmentReport> PhysicalEducationStat { get; set; }

        public GroupOfDepartmentsReport(IReadOnlyList<Department> departments, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations)
        {
           
            InitializeData(departments, healthGroups, physicalEducations);
        }

        private void InitializeData(IReadOnlyList<Department> departments, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations)
        {
            IReadOnlyList<DepartmentReport> departmentReports = GenerateDepartmentReports(departments, healthGroups, physicalEducations);
            if (departmentReports == null)
                All = default(int);
            else
            {
                foreach (var report in departmentReports)
                {
                    if (report != null)
                        All += report.All;
                }
            }
            CertificatePresenceStatistics = GetCertificatePresenceStat(departmentReports);
            HealthGroupStat = GetHealthGroupStat(departmentReports, healthGroups);
            PhysicalEducationStat = GetPhysicalEducationStat(departmentReports, physicalEducations);
        }


        private IReadOnlyList<DepartmentReport> GenerateDepartmentReports(IReadOnlyList<Department> departments, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations)
        {
            List<DepartmentReport> reports = new List<DepartmentReport>();
            foreach (var department in departments)
            {
                if (department != null)
                {
                    DepartmentReport report = new DepartmentReport(department, healthGroups, physicalEducations);
                    reports.Add(report);
                }
            }
            return reports;
        }

        private CertificatePresenceStat<DepartmentReport> GetCertificatePresenceStat(IReadOnlyList<DepartmentReport> departmentReports) // issue  with presence cert constructor!!!
        {
            if (departmentReports == null)
                return new CertificatePresenceStat<DepartmentReport>(new List<DepartmentReport>(), new List<DepartmentReport>(), 0, 0);

            List<DepartmentReport> haveList = departmentReports.Where(p => p.CertificatePresenceStatistics.Have.Count > 0).ToList();
            List<DepartmentReport> dontHaveList = departmentReports.Where(p => p.CertificatePresenceStatistics.DontHave.Count > 0).ToList();
            int haveCount = default(int);
            int dontHaveCount = default(int);
            foreach (var departmentReport in departmentReports)
            {
                if (departmentReport != null)
                {
                    var countStat = departmentReport.CertificatePresenceStatistics.CountsInEachStat;
                    haveCount += countStat[0];
                    dontHaveCount += countStat[1];
                }
            }
            return new CertificatePresenceStat<DepartmentReport>(haveList, dontHaveList, haveCount, dontHaveCount);
        }

        private HealthGroupStat<DepartmentReport> GetHealthGroupStat(IReadOnlyList<DepartmentReport> departmentReports, IReadOnlyList<HealthGroup> healthGroups)
        {
            if (healthGroups == null)
            {
                return new HealthGroupStat<DepartmentReport>(new List<HealthGroup>(), new List<List<DepartmentReport>>(), new List<int>());
            }
            var stats = new List<List<DepartmentReport>>();
            List<int> countInEachGroup = new List<int>();
            foreach (var healthGroup in healthGroups)
            {
                countInEachGroup.Add(0);
                stats.Add(new List<DepartmentReport>());
            }
            for (int i = 0; i < healthGroups.Count; i++)
            {
                var list = departmentReports[i].HealthGroupStat.CountsInEachStat;
                for (int j = 0; j < list.Count; j++)
                {
                    countInEachGroup[j] += list[j];
                    if (list[j] > 0)
                        stats[j].Add(departmentReports[i]);
                }
            }
            return new HealthGroupStat<DepartmentReport>(healthGroups, stats, countInEachGroup);
        }

        private PhysicalEducationStat<DepartmentReport> GetPhysicalEducationStat(IReadOnlyList<DepartmentReport> departmentReports, IReadOnlyList<PhysicalEducation> physicalEducations)
        {
            if (physicalEducations == null)
            {
                return new PhysicalEducationStat<DepartmentReport>(new List<PhysicalEducation>(), new List<List<DepartmentReport>>(), new List<int>());
            }
            var stats = new List<List<DepartmentReport>>();
            List<int> countInEachGroup = new List<int>();
            foreach (var physicalEducation in physicalEducations)
            {
                countInEachGroup.Add(0);
                stats.Add(new List<DepartmentReport>());
            }
            for (int i = 0; i < physicalEducations.Count; i++)
            {
                var list = departmentReports[i].PhysicalEducationStat.CountsInEachStat;
                for (int j = 0; j < list.Count; j++)
                {
                    countInEachGroup[j] += list[j];
                    if (list[j] > 0)
                        stats[j].Add(departmentReports[i]);
                }
            }
            return new PhysicalEducationStat<DepartmentReport>(physicalEducations, stats, countInEachGroup);
        }

    }
}
