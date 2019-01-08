using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Business;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Service.ReportModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.BusinessServices
{
    public class GroupOfStudentReportService<TEntity, TService> : IReportService<GroupOfStudentsReport, TEntity> where TEntity : class where TService : IGetAllStudents<TEntity>
    {

        private readonly TService _entityService;
        private readonly IHealthGroupService _healthGroupService;
        private readonly IPhysicalEducationService _physicalEducationService;
        private readonly IStudentService _studentService;


        public GroupOfStudentReportService(TService entityService, IHealthGroupService healthGroupService, IPhysicalEducationService physicalEducationService, IStudentService studentService)
        {
            _entityService = entityService;
            _healthGroupService = healthGroupService;
            _physicalEducationService = physicalEducationService;
            _studentService = studentService;
        }

        private async Task<IReadOnlyList<HealthGroup>> GetHealthGroupsAsync()
        {
            return await _healthGroupService.GetAllAsync();
        }

        private async Task<IReadOnlyList<PhysicalEducation>> GetPhysicalEducationsAsync()
        {
            return await _physicalEducationService.GetAllAsync();
        }

        public async Task<GroupOfStudentsReport> GetAllFromAsync(TEntity container)
        {
            IReadOnlyList<Student> students = _entityService.GetAllStudents(container);
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();

            GroupOfStudentsReport report = new GroupOfStudentsReport(students, healthGroups, physicalEducations);
            return report;
        }

        public async Task<GroupOfStudentsReport> GetInvalidFromAsync(TEntity container)
        {
            IReadOnlyList<Student> students = _studentService.SortStudents(_entityService.GetAllStudents(container), false, DateTime.Now);
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();

            GroupOfStudentsReport report = new GroupOfStudentsReport(students, healthGroups, physicalEducations);
            return report;
        }

        public async Task<GroupOfStudentsReport> GetInvalidOnDateFromAsync(TEntity container, DateTime dateTime)
        {
            IReadOnlyList<Student> students = _studentService.SortStudents(_entityService.GetAllStudents(container), false, dateTime);
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();

            GroupOfStudentsReport report = new GroupOfStudentsReport(students, healthGroups, physicalEducations);
            return report;
        }

        public Task<GroupOfStudentsReport> GetInvalidOnDateIntervalFromAsync(TEntity container, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupOfStudentsReport> GetValidFromAsync(TEntity container)
        {
            IReadOnlyList<Student> students = _entityService.GetAllStudents(container);
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();

            GroupOfStudentsReport report = new GroupOfStudentsReport(students, healthGroups, physicalEducations);
            return report;
        }

        public async Task<GroupOfStudentsReport> GetValidOnDateFromAsync(TEntity container, DateTime dateTime)
        {
            IReadOnlyList<Student> students = _entityService.GetAllStudents(container);
            IReadOnlyList<PhysicalEducation> physicalEducations = await GetPhysicalEducationsAsync();
            IReadOnlyList<HealthGroup> healthGroups = await GetHealthGroupsAsync();

            GroupOfStudentsReport report = new GroupOfStudentsReport(students, healthGroups, physicalEducations);
            return report;
        }

        public Task<GroupOfStudentsReport> GetValidOnDateIntervalFromAsync(TEntity container, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
