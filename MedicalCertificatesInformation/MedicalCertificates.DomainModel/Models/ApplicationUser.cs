using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Pseudonim { get; set; }

        public virtual List<Group> Groups { get; set; }
    }
}
