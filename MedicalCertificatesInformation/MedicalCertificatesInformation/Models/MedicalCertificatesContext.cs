using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MedicalCertificatesInformation.Models.Database.Models;

namespace MedicalCertificatesInformation.Models
{
    public class MedicalCertificatesContext : DbContext
    {
        public MedicalCertificatesContext() : base("name=MedicalCertificatesDB")
        {
        }


        public DbSet<HealthGroup> HealthGroups { get; set; }
        public DbSet<PhysicalEducation> PhysicalEducations { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MedicalCertificate> MedicalCertificates { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }




    }
}