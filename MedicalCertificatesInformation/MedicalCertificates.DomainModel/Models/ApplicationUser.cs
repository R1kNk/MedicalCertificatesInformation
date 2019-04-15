﻿using MedicalCertificates.DomainModel.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class ApplicationUser : IdentityUser, IPseudonim
    {
        public string Pseudonim { get; set; }
    }
}
