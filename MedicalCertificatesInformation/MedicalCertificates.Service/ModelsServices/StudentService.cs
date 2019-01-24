using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    public class StudentService : CRUDService<Student>, IStudentService
    {
        private readonly IMedicalCertificateService _medicalCertificateService;

        private readonly IRepository<Group> _groupRepository;


        public StudentService(IMedicalCertificateService medicalCertificateService, IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _medicalCertificateService = medicalCertificateService;
            _groupRepository = _unitOfWork.GetRepository<Group>();
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddStudentAsync(Student student, int groupId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            if(group == null)
              return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.GroupNotFound });

            student.GroupId = group.Id;
            student.Group = group;

            var result = await CreateAsync(student);
            if(result!=null)
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
            else
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.StudentNotFound});

        }

        public async Task<OperationResult<BusinessLogicResultError>> MoveStudentAsync(int studentId, int groupId)
        {
            var student = await GetByIdAsync(studentId);
            if (student == null)
               return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.StudentNotFound });

            var group = await _groupRepository.GetByIdAsync(groupId);
            if(group == null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.GroupNotFound });

            if(student.GroupId == group.Id) 
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.AlreadyInThisGroup });


            student.GroupId = group.Id;
            student.Group = group;
     
            await _unitOfWork.SaveAsync();
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public IReadOnlyList<Student> SortStudents(IReadOnlyList<Student> students, bool valid, DateTime dateTime)
        {
            List<Student> result = new List<Student>();
            if (students == null)
                return result;

            foreach (var student in students)
            {
                if (student != null)
                {
                    var lastCertificate = _medicalCertificateService.GetLastCertificate(student);
                    if (lastCertificate == null)
                    {
                        if (!valid)
                            result.Add(student);
                    }
                    else
                    {
                        if (lastCertificate.FinishDate < dateTime)
                        {
                            if (!valid)
                                result.Add(student);
                        }
                        else
                        {
                            if (valid)
                                result.Add(student);
                        }
                    }
                }
            }
            return result;
        }

    }
}
