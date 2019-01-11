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
        private readonly IRepository<MedicalCertificate> _medicalCertificatesRepository;

        public StudentService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _medicalCertificatesRepository = unitOfWork.GetRepository<MedicalCertificate>();
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddMedicalCertificateAsync(Student student, MedicalCertificate certificate)
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

        public async Task<OperationResult<BusinessLogicResultError>> UpdateMedicalCertificateAsync(Student student, MedicalCertificate updateCertificate)
        {
            if (updateCertificate.StartDate > updateCertificate.FinishDate || updateCertificate.CertificateTerm != updateCertificate.FinishDate - updateCertificate.StartDate)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.InvalidDate });
            var existingStudent = await _tEntityRepository.GetByIdAsync(student.Id);

            if(existingStudent == null)
            {
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.StudentNotFound });
            }
            var lastCertificate = GetLastCertificate(existingStudent);
            if(lastCertificate == null)
            {
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.NoCertificates });
            }
            var existingCertificate = await _medicalCertificatesRepository.GetByIdAsync(updateCertificate.Id);
            if(existingCertificate == null)
            {
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.CertificateNotFound });
            }
            //
            existingCertificate.HealthGroupId = updateCertificate.HealthGroupId;
            existingCertificate.HospitalId = updateCertificate.HospitalId;
            existingCertificate.PhysicalEducationId = updateCertificate.PhysicalEducationId;
            if(existingCertificate.Id != lastCertificate.Id)
            {
                if(existingCertificate.StartDate != updateCertificate.StartDate || existingCertificate.FinishDate != updateCertificate.FinishDate || existingCertificate.CertificateTerm != updateCertificate.CertificateTerm)
                {
                    return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.UpdatingExpiredCertificateDate });
                }
                _medicalCertificatesRepository.Update(existingCertificate);
                await _unitOfWork.SaveAsync();
                return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
            }
            else
            {
                var penultimateCertificate = existingStudent.MedicalCertificates.Where(p => p.Id != existingCertificate.Id).LastOrDefault();
                if(penultimateCertificate != null)
                {
                    if (penultimateCertificate.FinishDate > updateCertificate.StartDate)
                        return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.OverlappingDate });
                }
                existingCertificate.StartDate = updateCertificate.StartDate;
                existingCertificate.CertificateTerm = updateCertificate.CertificateTerm;
                existingCertificate.FinishDate = updateCertificate.FinishDate;
                _medicalCertificatesRepository.Update(existingCertificate);
                await _unitOfWork.SaveAsync();
                return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
            }
            //

        }

        public MedicalCertificate GetLastCertificate(Student student)
        {
           if(student !=null || student.MedicalCertificates != null)
           {
             return student.MedicalCertificates.LastOrDefault();
           }
             return null;
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
