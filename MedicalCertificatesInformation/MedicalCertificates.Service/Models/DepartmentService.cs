using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.Common;
using MedicalCertificates.Service.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.Models
{
    class DepartmentService : CRUDService<Department>, IDepartmentService
    {
        public DepartmentService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
