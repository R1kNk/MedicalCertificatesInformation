using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Business;

namespace MedicalCertificates.Service.BusinessServices
{
    class DepartmentReportService : IReportService<MedicalCertificate, Department>
    {
        public Task<IReadOnlyCollection<MedicalCertificate>> GetAllFrom(Department container)
        {
            //List<Student> medicalCertificates = new List<Student>();
            //foreach(var course in container.Courses)
            //{
            //    if(course!=null)
            //    foreach(var group in course.Groups)
            //    {
            //        if(group!=null)
            //        foreach (var student in group.Students)
            //        {
            //           if (student != null)
            //           {
            //              var certificate = student.MedicalCertificates.LastOrDefault();
            //              if (certificate != null)
            //              medicalCertificates.Add(certificate);
            //           }
            //        }
            //    }
            //}
            throw new NotImplementedException();

        }

        public Task<IReadOnlyCollection<MedicalCertificate>> GetInvalidFrom(Department container)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MedicalCertificate>> GetInvalidOnDateFrom(Department container, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MedicalCertificate>> GetInvalidOnDateIntervalFrom(Department container, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MedicalCertificate>> GetValidFrom(Department container)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MedicalCertificate>> GetValidOnDateFrom(Department container, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MedicalCertificate>> GetValidOnDateIntervalFrom(Department container, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
