﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalCertificatesInformation.Models.Database.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string DepartmentLetter { get; set; }
        public int Number { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<Student> Students { get; set; }
    }
}