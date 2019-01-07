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

namespace MedicalCertificates.Service.Models
{
    public class StudentService : CRUDService<Student>, IStudentService
    {
        public StudentService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddMedicalSertificateAsync(Student student, MedicalCertificate certificate)
        {
            if (certificate.StartDate > certificate.FinishDate || certificate.CertificateTerm != certificate.FinishDate - certificate.StartDate)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>(){ BusinessLogicResultError.InvalidDate});
            var entity = await _tEntityRepository.GetByIdAsync(student.Id);
            var lastCertificate = entity.MedicalCertificates.LastOrDefault();
            if (lastCertificate != null)
            {
                if (lastCertificate.FinishDate > certificate.StartDate)
                    return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.OverlappingDate });
            }
            certificate.Student = entity;
            entity.MedicalCertificates.Add(certificate);
            await _unitOfWork.SaveAsync();
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public MedicalCertificate GetLastCertificate(Student student)
        {
           if(student !=null || student.MedicalCertificates != null)
           {
             return student.MedicalCertificates.LastOrDefault();
           }
             return null;
        }

        internal IReadOnlyList<Student> SortStudents(IReadOnlyList<Student> students, bool valid, DateTime dateTime)
        {
            List<Student> result = new List<Student>();
            if (students == null)
                return result;

            foreach (var student in students)
            {
                if (student != null)
                {
                    var lastCertificate = GetLastCertificate(student);
                    if (lastCertificate == null)
                    {
                        if (!valid)
                            result.Add(student);
                    }
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
            return result;
        }
    }
}
