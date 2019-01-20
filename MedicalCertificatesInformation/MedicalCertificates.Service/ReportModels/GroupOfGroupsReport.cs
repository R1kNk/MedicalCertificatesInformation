using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ReportModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalCertificates.Service.ReportModels
{
    public class GroupOfGroupsReport
    {
        public int All { get; set; }
        
        public CertificatePresenceStat<GroupReport> CertificatePresenceStatistics { get; set; }
        public HealthGroupStat<GroupReport> HealthGroupStat { get; set; }
        public PhysicalEducationStat<GroupReport> PhysicalEducationStat { get; set; }

        public GroupOfGroupsReport(IReadOnlyList<Group> groups, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations, DateTime dateTime)
        {
            InitializeData(groups, healthGroups, physicalEducations, dateTime);
        }

        private void InitializeData(IReadOnlyList<Group> groups, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations, DateTime dateTime)
        {
            IReadOnlyList<GroupReport> groupReports = GenerateGroupReports(groups, healthGroups, physicalEducations, dateTime);
            if (groupReports == null)
                All = default(int);
            else
            {
                foreach (var report in groupReports)
                {
                    if (report != null)
                        All += report.All;
                }
            }
            CertificatePresenceStatistics = GetCertificatePresenceStat(groupReports);
            HealthGroupStat = GetHealthGroupStat(groupReports, healthGroups);
            PhysicalEducationStat = GetPhysicalEducationStat(groupReports, physicalEducations);
        }


        private IReadOnlyList<GroupReport> GenerateGroupReports(IReadOnlyList<Group> groups, IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<PhysicalEducation> physicalEducations, DateTime dateTime)
        {
            List<GroupReport> reports = new List<GroupReport>();
            foreach (var group in groups)
            {
                if (group != null)
                {
                    GroupReport report = new GroupReport(group, healthGroups, physicalEducations, dateTime);
                    reports.Add(report);
                }
            }
            return reports;
        }

        private CertificatePresenceStat<GroupReport> GetCertificatePresenceStat(IReadOnlyList<GroupReport> groupReports) // issue  with presence cert constructor!!!
        {
            if (groupReports == null)
                return new CertificatePresenceStat<GroupReport>(new List<GroupReport>(), new List<GroupReport>(), 0, 0);

            List<GroupReport> haveList = groupReports.Where(p => p.CertificatePresenceStatistics.Have.Count > 0).ToList();
            List<GroupReport> dontHaveList = groupReports.Where(p => p.CertificatePresenceStatistics.DontHave.Count > 0).ToList();
            int haveCount = default(int);
            int dontHaveCount = default(int);
            foreach (var groupReport in groupReports)
            {
                if (groupReport != null)
                {
                    var countStat = groupReport.CertificatePresenceStatistics.CountsInEachStat;
                    haveCount += countStat[0];
                    dontHaveCount += countStat[1];
                }
            }
            return new CertificatePresenceStat<GroupReport>(haveList, dontHaveList, haveCount, dontHaveCount);
        }

        private HealthGroupStat<GroupReport> GetHealthGroupStat(IReadOnlyList<GroupReport> groupReports, IReadOnlyList<HealthGroup> healthGroups)
        {
            if (healthGroups == null)
            {
                return new HealthGroupStat<GroupReport>(new List<HealthGroup>(), new List<List<GroupReport>>(), new List<int>());
            }
            var stats = new List<List<GroupReport>>();
            List<int> countInEachGroup = new List<int>();
            foreach (var healthGroup in healthGroups)
            {
                countInEachGroup.Add(0);
                stats.Add(new List<GroupReport>());
            }
            for (int i = 0; i < healthGroups.Count; i++)
            {
                var list = groupReports[i].HealthGroupStat.CountsInEachStat;
                for (int j = 0; j < list.Count; j++)
                {
                    countInEachGroup[j] += list[j];
                    if (list[j] > 0)
                        stats[j].Add(groupReports[i]);
                }
            }
            return new HealthGroupStat<GroupReport>(healthGroups, stats, countInEachGroup);
        }

        private PhysicalEducationStat<GroupReport> GetPhysicalEducationStat(IReadOnlyList<GroupReport> groupReports, IReadOnlyList<PhysicalEducation> physicalEducations)
        {
            if (physicalEducations == null)
            {
                return new PhysicalEducationStat<GroupReport>(new List<PhysicalEducation>(), new List<List<GroupReport>>(), new List<int>());
            }
            var stats = new List<List<GroupReport>>();
            List<int> countInEachGroup = new List<int>();
            foreach (var physicalEducation in physicalEducations)
            {
                countInEachGroup.Add(0);
                stats.Add(new List<GroupReport>());
            }
            for (int i = 0; i < physicalEducations.Count; i++)
            {
                var list = groupReports[i].PhysicalEducationStat.CountsInEachStat;
                for (int j = 0; j < list.Count; j++)
                {
                    countInEachGroup[j] += list[j];
                    if (list[j] > 0)
                        stats[j].Add(groupReports[i]);
                }
            }
            return new PhysicalEducationStat<GroupReport>(physicalEducations, stats, countInEachGroup);
        }
    }
}
