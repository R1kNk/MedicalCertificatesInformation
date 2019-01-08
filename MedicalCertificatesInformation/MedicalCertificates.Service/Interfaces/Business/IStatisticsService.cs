using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ReportModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Business
{
    public interface IStatisticsService
    {
        StudentReport GenerateStudentStatistics(Student student);

        Task<GroupOfStudentsReport> GenerateGroupOFStudentsStatisticsAsync(IReadOnlyList<Student> students);

        Task<GroupReport> GenerateGroupStatisticsAsync(Group group);

        Task<GroupOfGroupsReport> GenerateGroupOfGroupsStatisticsAsync(IReadOnlyList<Group> group);

        Task<DepartmentReport> GenerateDepartmentStatisticsAsync(Department department);

        Task<GroupOfDepartmentsReport> GenerateGroupOfDepartmentsStatisticsAsync(IReadOnlyList<Department> departments);

    }
}
