﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalCertificatesInformation.Models.Database.Models
{
    public class MedicalCertificate
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public TimeSpan SertificateTerm { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    
        public int HealthGroupId { get; set; }
        public HealthGroup HealthGroup { get; set; }

        public int PhysicalEducationId { get; set; }
        public PhysicalEducation PhysicalEducation { get; set; }

        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}