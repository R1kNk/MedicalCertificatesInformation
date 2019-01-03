using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MedicalSertificates.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Group> Groups { get; set; }
    }
}
