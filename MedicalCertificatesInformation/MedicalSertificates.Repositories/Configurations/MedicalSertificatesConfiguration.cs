using MedicalSertificates.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalSertificates.Repositories.Configurations
{
    public class MedicalSertificatesConfiguration
    {

        public class HealthGroupConfiguration : IEntityTypeConfiguration<HealthGroup>
        {
            public void Configure(EntityTypeBuilder<HealthGroup> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            }
        }

        public class PhysicalEducationConfiguration : IEntityTypeConfiguration<PhysicalEducation>
        {
            public void Configure(EntityTypeBuilder<PhysicalEducation> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            }
        }

        public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
        {
            public void Configure(EntityTypeBuilder<Hospital> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            }
        }

        public class StudentConfiguration : IEntityTypeConfiguration<Student>
        {
            public void Configure(EntityTypeBuilder<Student> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.Property(p => p.Surname).IsRequired().HasMaxLength(60);
                builder.Property(p => p.GoogleDriveFolderId).IsRequired();
            }
        }

        public class MedicalCertificateConfiguration : IEntityTypeConfiguration<MedicalCertificate>
        {
            public void Configure(EntityTypeBuilder<MedicalCertificate> builder)
            {
               builder.Property(p => p.StartDate).IsRequired();
            }
        }

        public class GroupConfiguration : IEntityTypeConfiguration<Group>
        {
            public void Configure(EntityTypeBuilder<Group> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(5);
               // builder.HasIndex(p => p.Name).IsUnique();
                builder.Property(p => p.GoogleDriveFolderId).IsRequired();
            }
        }

        public class CourseConfiguration : IEntityTypeConfiguration<Course>
        {

            public void Configure(EntityTypeBuilder<Course> builder)
            {
                builder.Property(p => p.Number).IsRequired();
            }
        }

        public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
        {

            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.Property(p => p.Name).IsRequired().HasMaxLength(60);
            }
        }
    }

  
}
