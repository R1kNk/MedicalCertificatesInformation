using MedicalCertificatesInformation.Models.Database.Models;
using System.Data.Entity.ModelConfiguration;

namespace MedicalCertificatesInformation.Models.Database.Shared
{
    public class ContextConfigurations
    {
        public class HealthGroupConfiguration : EntityTypeConfiguration<HealthGroup>
        {
            public HealthGroupConfiguration()
            {
                Property(p => p.Name).IsRequired().HasMaxLength(50);
            }
        }

        public class PhysicalEducationConfiguration : EntityTypeConfiguration<PhysicalEducation>
        {
            public PhysicalEducationConfiguration()
            {
                Property(p => p.Name).IsRequired().HasMaxLength(50);
            }
        }

        public class HospitalConfiguration : EntityTypeConfiguration<Hospital>
        {
            public HospitalConfiguration()
            {
                Property(p => p.Name).IsRequired().HasMaxLength(50);
            }
        }

        public class StudentConfiguration : EntityTypeConfiguration<Student>
        {
            public StudentConfiguration()
            {
                Property(p => p.Name).IsRequired().HasMaxLength(50);
                Property(p => p.Surname).IsRequired().HasMaxLength(60);
            }
        }

        public class MedicalCertificateConfiguration : EntityTypeConfiguration<MedicalCertificate>
        {
            public MedicalCertificateConfiguration()
            {
                Property(p => p.StartDate).IsRequired();
            }
        }

        public class GroupConfiguration : EntityTypeConfiguration<Group>
        {
            public GroupConfiguration()
            {
                Property(p => p.DepartmentLetter).IsRequired().HasMaxLength(1);
                Property(p => p.Number).IsRequired();
                Property(p => p.CuratorId).IsOptional();

            }
        }

        public class CourseConfiguration : EntityTypeConfiguration<Course>
        {
            public CourseConfiguration()
            {
                Property(p => p.Number).IsRequired();
            }
        }

        public class DepartmentConfiguration : EntityTypeConfiguration<Department>
        {
            public DepartmentConfiguration()
            {
                Property(p => p.Name).IsRequired().HasMaxLength(50);
            }
        }
    }
}