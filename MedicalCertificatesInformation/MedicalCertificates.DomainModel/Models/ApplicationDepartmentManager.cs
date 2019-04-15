using MedicalCertificates.DomainModel.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class DepartmentManagerUser : ApplicationUser
    {
        public virtual Department Department { get; set; }
    }
}
