﻿using System.Collections.Generic;

namespace MedicalCertificatesInformation.Models.Database.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GoogleDriveFolderId { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public List<MedicalCertificate> MedicalCertificates { get; set; }
    }
}