using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.DomainModel.Models
{
    public class DefaultUser : ApplicationUser
    {
        public virtual List<Group> Groups { get; set; }
    }
}
