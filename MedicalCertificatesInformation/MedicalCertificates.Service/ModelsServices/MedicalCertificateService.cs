using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.ModelsServices
{
    public class MedicalCertificateService : CRUDService<MedicalCertificate>, IMedicalCertificateService
    {
        private readonly IRepository<Student> _studentRepository;

        public MedicalCertificateService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _studentRepository = unitOfWork.GetRepository<Student>();
        }

        private bool IsCertificateValid(MedicalCertificate medicalCertificate)
        {
            if (medicalCertificate == null)
                return false;

            if (medicalCertificate.StartDate > medicalCertificate.FinishDate || medicalCertificate.CertificateTerm != medicalCertificate.FinishDate.Subtract(medicalCertificate.StartDate).TotalDays)
                return false;
            else return true;
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddMedicalCertificateAsync(MedicalCertificate certificate, int studentId)
        {
            if(!IsCertificateValid(certificate))
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.InvalidDate });

            var foundedStudent = await _studentRepository.GetByIdAsync(studentId);

            var lastCertificate = GetLastCertificate(foundedStudent);

            if (lastCertificate != null)
            {
                if (lastCertificate.FinishDate > certificate.FinishDate)
                    return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.OverlappingDate });
            }

            certificate.StudentId = foundedStudent.Id;
            var result = await CreateAsync(certificate);

            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<BusinessLogicResultError>> EditMedicalCertificateAsync(MedicalCertificate updateCertificate)
        {
            if (!IsCertificateValid(updateCertificate))
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.InvalidDate });

            var existingCertificate = await GetByIdAsync(updateCertificate.Id);
            if (existingCertificate == null)
            {
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.CertificateNotFound });
            }

            var student = existingCertificate.Student;

            var lastCertificate = GetLastCertificate(student);
            if (lastCertificate == null)
            {
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.NoCertificates });
            }

            existingCertificate.HealthGroupId = updateCertificate.HealthGroupId;
            existingCertificate.PhysicalEducationId = updateCertificate.PhysicalEducationId;

            if (existingCertificate.Id != lastCertificate.Id)
            {
                if (existingCertificate.StartDate != updateCertificate.StartDate || existingCertificate.FinishDate != updateCertificate.FinishDate || existingCertificate.CertificateTerm != updateCertificate.CertificateTerm)
                {
                    return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.ExpiredCertificate });
                }

                var result = await UpdateAsync(existingCertificate);

                return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();

            }
            else
            {
                var penultimateCertificate = GetPenultimateCertificate(student);
                if (penultimateCertificate != null)
                {
                    if (penultimateCertificate.FinishDate > updateCertificate.FinishDate)
                        return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.OverlappingDate });
                }

                existingCertificate.StartDate = updateCertificate.StartDate;
                existingCertificate.CertificateTerm = updateCertificate.CertificateTerm;
                existingCertificate.FinishDate = updateCertificate.FinishDate;

                var result = UpdateAsync(existingCertificate);
                return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
            }
        }

        public async Task<OperationResult<BusinessLogicResultError>> DeleteMedicalCertificateAsync(int deleteCertificateId)
        {
            var existingCertificate = await GetByIdAsync(deleteCertificateId);

            if(existingCertificate == null)
            {
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.CertificateNotFound });
            }

            var student = existingCertificate.Student;

            var lastCertificate = GetLastCertificate(student);
            if(lastCertificate == null || lastCertificate.Id == existingCertificate.Id)
            {
                var result = await DeleteAsync(existingCertificate);
                return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
            }
            return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.ExpiredCertificate });
            
        }


        public MedicalCertificate GetLastCertificate(Student student)
        {
            if (student != null && student.MedicalCertificates != null)
            {
                var certs = student.MedicalCertificates.OrderBy(p => p.Id).ToList();
                return certs.LastOrDefault();
            }
            return null;
        }

        public MedicalCertificate GetPenultimateCertificate(Student student)
        {
            if (student != null || student.MedicalCertificates != null)
            {
                var lastCertificate =  GetLastCertificate(student);
                if (lastCertificate == null)
                    return null;
                var penultimateCertificate = student.MedicalCertificates.Where(p => p.Id != lastCertificate.Id).LastOrDefault();
                return penultimateCertificate;
            }
            return null;
        }
    }
}
