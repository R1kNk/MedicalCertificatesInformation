using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static MedicalCertificates.Repositories.Configurations.MedicalCertificatesConfiguration;

namespace MedicalCertificates.Repositories
{
    public class MedicalCertificatesDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IDbContext
    {

        public readonly static string connectionString = "Server=.\\SQLEXPRESS;Database=MedicalCertificates.Web;Trusted_Connection=True;MultipleActiveResultSets=true";

        public MedicalCertificatesDbContext(DbContextOptions<MedicalCertificatesDbContext> options)
           : base(options)
        {
            
        }

        public DbSet<HealthGroup> HealthGroups { get; set; }
        public DbSet<PhysicalEducation> PhysicalEducations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MedicalCertificate> MedicalCertificates { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HealthGroupConfiguration());
            modelBuilder.ApplyConfiguration(new PhysicalEducationConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalCertificateConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public static DbContextOptionsBuilder<MedicalCertificatesDbContext> GetOptionsBuilder()
        {
            var builder = new DbContextOptionsBuilder<MedicalCertificatesDbContext>();
            builder.UseSqlServer(connectionString);
            builder.UseLazyLoadingProxies();
            return builder;
        }
    }

    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MedicalCertificatesDbContext>
    {
        public MedicalCertificatesDbContext CreateDbContext(string[] args)
        {
            var context = new MedicalCertificatesDbContext(MedicalCertificatesDbContext.GetOptionsBuilder().Options);
            return context;
        }
    }
}
