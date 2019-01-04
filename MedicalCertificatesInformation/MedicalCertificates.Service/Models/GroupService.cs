﻿using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.Common;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.Models
{
    class GroupService : CRUDService<Group>, IGroupService
    {
        public GroupService(IMedicalCertificatesUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
