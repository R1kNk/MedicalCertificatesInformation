﻿using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class HealthGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<MedicalCertificate> MedicalCertificates { get; set; }
    }
}