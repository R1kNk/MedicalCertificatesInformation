using MedicalCertificates.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCertificates.Repositories.Configurations
{
    public class MedicalCertificatesConfiguration
    {

        public class HealthGroupConfiguration : IEntityTypeConfiguration<HealthGroup>
        {
            public void Configure(EntityTypeBuilder<HealthGroup> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.HasIndex(p => p.Name).IsUnique();
            }
        }

        public class PhysicalEducationConfiguration : IEntityTypeConfiguration<PhysicalEducation>
        {
            public void Configure(EntityTypeBuilder<PhysicalEducation> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.HasIndex(p => p.Name).IsUnique();
            }
        }

        public class StudentConfiguration : IEntityTypeConfiguration<Student>
        {
            public void Configure(EntityTypeBuilder<Student> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.Property(p => p.Surname).IsRequired().HasMaxLength(60);
                //builder.Property(p => p.GoogleDriveFolderId).IsRequired();
                builder.HasMany(b => b.MedicalCertificates).WithOne(p => p.Student).HasForeignKey(p => p.StudentId).OnDelete(DeleteBehavior.Cascade);

            }
        }

        public class MedicalCertificateConfiguration : IEntityTypeConfiguration<MedicalCertificate>
        {
            public void Configure(EntityTypeBuilder<MedicalCertificate> builder)
            {
               builder.Property(p => p.StartDate).IsRequired();
               builder.Property(p => p.CertificateTerm).IsRequired();
               builder.Property(p => p.FinishDate).IsRequired();
               builder.Property(p => p.HealthGroupId).IsRequired();
               builder.Property(p => p.PhysicalEducationId).IsRequired();
            }
        }

        public class GroupConfiguration : IEntityTypeConfiguration<Group>
        {
            public void Configure(EntityTypeBuilder<Group> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(5);
                builder.HasIndex(p => p.Name).IsUnique();
                //builder.Property(p => p.GoogleDriveFolderId).IsRequired();
                builder.HasMany(b => b.Students).WithOne(p => p.Group).HasForeignKey(p => p.GroupId).OnDelete(DeleteBehavior.Cascade);
                //builder.HasOne(p => p.Curator).WithMany(p => p.Groups).HasForeignKey(p => p.CuratorId);
            }
        }

        public class DefaultUserConfiguration : IEntityTypeConfiguration<DefaultUser>
        {
            public void Configure(EntityTypeBuilder<DefaultUser> builder)
            {

               builder.HasMany(b => b.Groups).WithOne(p => p.ApplicationUser).HasForeignKey(p => p.ApplicationUserId).OnDelete(DeleteBehavior.SetNull);
                //builder.HasOne(p => p.Curator).WithMany(p => p.Groups).HasForeignKey(p => p.CuratorId);
            }
        }

        public class DepartmentManagerUserConfiguration : IEntityTypeConfiguration<DepartmentManagerUser>
        {
            public void Configure(EntityTypeBuilder<DepartmentManagerUser> builder)
            {

                builder.HasOne(b => b.Department).WithOne(p => p.DepartmentManager).HasForeignKey<Department>(p => p.DepartmentManagerId).OnDelete(DeleteBehavior.SetNull);
                //builder.HasOne(p => p.Curator).WithMany(p => p.Groups).HasForeignKey(p => p.CuratorId);
            }
        }

        public class CourseConfiguration : IEntityTypeConfiguration<Course>
        {

            public void Configure(EntityTypeBuilder<Course> builder)
            {
                builder.Property(p => p.Number).IsRequired();
                builder.HasMany(b => b.Groups).WithOne(p => p.Course).HasForeignKey(p => p.CourseId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
        {

            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(60);
                builder.HasIndex(p => p.Name).IsUnique();
                builder.HasMany(b => b.Courses).WithOne(p => p.Department).HasForeignKey(p => p.DepartmentId).OnDelete(DeleteBehavior.Cascade);

            }
        }
    }

  
}
