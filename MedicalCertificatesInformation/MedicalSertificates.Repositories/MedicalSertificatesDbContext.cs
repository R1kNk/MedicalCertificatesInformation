using MedicalSertificates.DomainModel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static MedicalSertificates.Repositories.Configurations.MedicalSertificatesConfiguration;

namespace MedicalSertificates.Repositories
{
    public class MedicalSertificatesDbContext : IdentityDbContext<ApplicationUser>
    {

        public MedicalSertificatesDbContext(DbContextOptions<MedicalSertificatesDbContext> options)
           : base(options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HealthGroupConfiguration());
            modelBuilder.ApplyConfiguration(new PhysicalEducationConfiguration());
            modelBuilder.ApplyConfiguration(new HospitalConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalCertificateConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
