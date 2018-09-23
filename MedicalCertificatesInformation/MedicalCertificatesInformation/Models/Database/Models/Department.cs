using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalCertificatesInformation.Models.Database.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}