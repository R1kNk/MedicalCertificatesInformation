using System.Data.Entity;
using MedicalCertificatesInformation.Models.Database.Models;
using static MedicalCertificatesInformation.Models.Database.Shared.ContextConfigurations;

namespace MedicalCertificatesInformation.Models
{
    public class MedicalCertificatesContext : DbContext
    {
        public MedicalCertificatesContext() : base("name=MedicalCertificatesDB")
        {
            Configuration.LazyLoadingEnabled = false;
        }


        public DbSet<HealthGroup> HealthGroups { get; set; }
        public DbSet<PhysicalEducation> PhysicalEducations { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MedicalCertificate> MedicalCertificates { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HealthGroupConfiguration());
            modelBuilder.Configurations.Add(new PhysicalEducationConfiguration());
            modelBuilder.Configurations.Add(new HospitalConfiguration());
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new MedicalCertificateConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
        }


    }
}