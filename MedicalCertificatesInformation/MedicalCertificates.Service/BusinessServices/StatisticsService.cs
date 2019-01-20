using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Business;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Service.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.BusinessServices
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IDepartmentService _departmentService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly IHealthGroupService _healthGroupService;
        private readonly IPhysicalEducationService _physicalEducationService;

        private async Task<IReadOnlyList<HealthGroup>> GetHealthGroupsAsync()
        {
            return await _healthGroupService.GetAllAsync();
        }

        private async Task<IReadOnlyList<PhysicalEducation>> GetPhysicalEducationsAsync()
        {
            return await _physicalEducationService.GetAllAsync();
        }

        public StatisticsService(IDepartmentService departmentService, IGroupService groupService, IStudentService studentService, IHealthGroupService healthGroupService, IPhysicalEducationService physicalEducationService)
        {
            _departmentService = departmentService;
            _groupService = groupService;
            _studentService = studentService;
            _healthGroupService = healthGroupService;
            _physicalEducationService = physicalEducationService;
            _studentService = studentService;
        }

        public async Task<DepartmentReport> GenerateDepartmentStatisticsAsync(Department department)
        {
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();
            DepartmentReport report = new DepartmentReport(department, healthGroups, physicalEducations, DateTime.Now);
            return report;
        }

        public async Task<GroupOfDepartmentsReport> GenerateGroupOfDepartmentsStatisticsAsync(IReadOnlyList<Department> departments)
        {
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();
            GroupOfDepartmentsReport report = new GroupOfDepartmentsReport(departments, healthGroups, physicalEducations);
            return report;
        }

        public async Task<GroupOfGroupsReport> GenerateGroupOfGroupsStatisticsAsync(IReadOnlyList<Group> groups)
        {
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();
            GroupOfGroupsReport report = new GroupOfGroupsReport(groups, healthGroups, physicalEducations, DateTime.Now);
            return report;
        }

        public async Task<GroupOfStudentsReport> GenerateGroupOFStudentsStatisticsAsync(IReadOnlyList<Student> students)
        {
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();
            GroupOfStudentsReport report = new GroupOfStudentsReport(students, healthGroups, physicalEducations, DateTime.Now);
            return report;
        }

        public async Task<GroupReport> GenerateGroupStatisticsAsync(Group group)
        {
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();
            GroupReport report = new GroupReport(group, healthGroups, physicalEducations, DateTime.Now);
            return report;
        }

        public StudentReport GenerateStudentStatistics(Student student)
        {
            MedicalCertificate lastMedicalCertificate = student.MedicalCertificates.LastOrDefault();
            StudentReport report = new StudentReport(student, lastMedicalCertificate, DateTime.Now);
            return report;
        }

    }
}
