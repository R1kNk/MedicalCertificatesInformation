using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static MedicalCertificates.Repositories.Configurations.MedicalCertificatesConfiguration;

namespace MedicalCertificates.Repositories
{
    public class MedicalCertificatesDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {

        public MedicalCertificatesDbContext(DbContextOptions<MedicalCertificatesDbContext> options)
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
