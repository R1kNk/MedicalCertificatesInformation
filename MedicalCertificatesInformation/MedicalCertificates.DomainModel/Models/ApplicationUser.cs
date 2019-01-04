using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Group> Groups { get; set; }
    }
}
