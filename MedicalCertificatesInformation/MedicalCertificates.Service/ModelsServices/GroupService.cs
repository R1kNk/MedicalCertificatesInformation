using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    class GroupService : CRUDService<Group>, IGroupService, IGetAllStudents<Group>
    {
        public GroupService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationResult<string>> AddStudentAsync(Group group, Student student)
        {
            var entity = await GetByIdAsync(group.Id);
            student.Group = entity;
            entity.Students.Add(student);
            await _unitOfWork.SaveAsync();
            return OperationResult<string>.CreateSuccessfulResult();
        }

        public IReadOnlyList<Student> GetAllStudents(Group group)
        {
            List<Student> studentList = new List<Student>();
            if (group == null)
                return studentList;
            
                if (group != null)
                    studentList.AddRange(group.Students.ToList());
            return studentList;
        }
    }
}
