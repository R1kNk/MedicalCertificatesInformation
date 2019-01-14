using AutoMapper;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Web.Models.CourseViewModels;
using MedicalCertificates.Web.Models.DepartmentViewModels;
using MedicalCertificates.Web.Models.GroupViewModels;
using MedicalCertificates.Web.Models.HealthGroupViewModels;
using MedicalCertificates.Web.Models.HospitalViewModels;
using MedicalCertificates.Web.Models.MedicalCertificatesViewModels;
using MedicalCertificates.Web.Models.PhysicalEducationViewModels;
using MedicalCertificates.Web.Models.StudentViewModels;

namespace MedicalCertificates.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Hospital
            CreateMap<Hospital, CreateHospitalViewModel>()
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.TelephoneNumber, map => map.MapFrom(p => p.TelephoneNumber))
                .ReverseMap();

            CreateMap<Hospital, DetailsHospitalViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ForMember(p => p.TelephoneNumber, map => map.MapFrom(p => p.TelephoneNumber))
               .ForMember(p => p.MedicalCertificates, map => map.MapFrom(p => p.MedicalCertificates))
               .ReverseMap();

            CreateMap<Hospital, EditHospitalViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ForMember(p => p.TelephoneNumber, map => map.MapFrom(p => p.TelephoneNumber))
               .ReverseMap();

            CreateMap<Hospital, DeleteHospitalViewModel>()
              .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
              .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
              .ForMember(p => p.TelephoneNumber, map => map.MapFrom(p => p.TelephoneNumber))
              .ReverseMap();

            //Physicaleducation
            CreateMap<PhysicalEducation, CreatePhysicalEducationViewModel>()
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ReverseMap();

            CreateMap<PhysicalEducation, DetailsPhysicalEducationViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ForMember(p => p.MedicalCertificates, map => map.MapFrom(p => p.MedicalCertificates))
               .ReverseMap();

            CreateMap<PhysicalEducation, EditPhysicaleducationViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ReverseMap();

            CreateMap<PhysicalEducation, DeletePhysicalEducationViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ReverseMap();

            //HealthGroup
            CreateMap<HealthGroup, CreateHealthGroupViewModel>()
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ReverseMap();

            CreateMap<HealthGroup, DetailsHealthGroupViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ForMember(p => p.MedicalCertificates, map => map.MapFrom(p => p.MedicalCertificates))
               .ReverseMap();

            CreateMap<HealthGroup, EditHealthGroupViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ReverseMap();

            CreateMap<HealthGroup, DeleteHealthGroupViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ReverseMap();

            //MedicalCertificate
            CreateMap<MedicalCertificate, CreateMedicalCertificateViewModel>()
                .ForMember(p => p.StudentId, map => map.MapFrom(p => p.StudentId))
                .ForMember(p => p.StartDate, map => map.MapFrom(p => p.StartDate))
                .ForMember(p => p.FinishDate, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.CertificateTerm, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.HealthGroupId, map => map.MapFrom(p => p.HealthGroupId))
                .ForMember(p => p.PhysicalEducationId, map => map.MapFrom(p => p.PhysicalEducationId))
                .ForMember(p => p.HospitalId, map => map.MapFrom(p => p.HospitalId))
                .ReverseMap();

            CreateMap<MedicalCertificate, DetailsMedicalCertificatesViewModel>()
               .ForMember(p => p.StartDate, map => map.MapFrom(p => p.StartDate))
                .ForMember(p => p.FinishDate, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.CertificateTerm, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.HealthGroup, map => map.MapFrom(p => p.HealthGroup))
                .ForMember(p => p.PhysicalEducation, map => map.MapFrom(p => p.PhysicalEducation))
                .ForMember(p => p.Hospital, map => map.MapFrom(p => p.Hospital))
                .ReverseMap();

            CreateMap<MedicalCertificate, DetailsMedicalCertificatesViewModel>()
               .ForMember(p => p.StartDate, map => map.MapFrom(p => p.StartDate))
                .ForMember(p => p.FinishDate, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.CertificateTerm, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.HealthGroup, map => map.MapFrom(p => p.HealthGroup))
                .ForMember(p => p.PhysicalEducation, map => map.MapFrom(p => p.PhysicalEducation))
                .ForMember(p => p.Hospital, map => map.MapFrom(p => p.Hospital))
                .ReverseMap();

            CreateMap<MedicalCertificate, EditMedicalCertificatesViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.StartDate, map => map.MapFrom(p => p.StartDate))
                .ForMember(p => p.FinishDate, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.CertificateTerm, map => map.MapFrom(p => p.FinishDate))
                .ForMember(p => p.HealthGroupId, map => map.MapFrom(p => p.HealthGroupId))
                .ForMember(p => p.PhysicalEducationId, map => map.MapFrom(p => p.PhysicalEducationId))
                .ForMember(p => p.HospitalId, map => map.MapFrom(p => p.HospitalId))
                .ReverseMap();

            CreateMap<MedicalCertificate, DeleteMedicalCertificateViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.StartDate, map => map.MapFrom(p => p.StartDate))
               .ForMember(p => p.FinishDate, map => map.MapFrom(p => p.FinishDate))
               .ForMember(p => p.CertificateTerm, map => map.MapFrom(p => p.FinishDate))
               .ForMember(p => p.Student, map => map.MapFrom(p => p.Student))
               .ReverseMap();

            //Student
            CreateMap<Student, CreateStudentViewModel>()
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.Surname, map => map.MapFrom(p => p.Surname))
                .ForMember(p => p.GroupId, map => map.MapFrom(p => p.GroupId))
                .ReverseMap();

            CreateMap<Student, DetailsStudentViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.Surname, map => map.MapFrom(p => p.Surname))
                .ForMember(p => p.Group, map => map.MapFrom(p => p.Group))
                .ForMember(p => p.MedicalCertificates, map => map.MapFrom(p => p.MedicalCertificates))
                .ReverseMap();

            CreateMap<Student, DeleteStudentViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.Surname, map => map.MapFrom(p => p.Surname))
                .ReverseMap();

            CreateMap<Student, EditStudentViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.Surname, map => map.MapFrom(p => p.Surname))
                .ReverseMap();

            CreateMap<Student, MoveStudentViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
               .ForMember(p => p.Surname, map => map.MapFrom(p => p.Surname))
               .ForMember(p => p.GroupId, map => map.MapFrom(p => p.GroupId))
               .ReverseMap();

            //Group
            CreateMap<Group, CreateGroupViewModel>()
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.CourseId, map => map.MapFrom(p => p.CourseId))
                .ReverseMap();

            CreateMap<Group, DetailsGroupViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.Course, map => map.MapFrom(p => p.Course))
                .ForMember(p => p.Students, map => map.MapFrom(p => p.Students))
                .ReverseMap();

            CreateMap<Group, DeleteGroupViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ReverseMap();

            CreateMap<Group, EditGroupViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ReverseMap();

            //Course
            CreateMap<Course, CreateCourseViewModel>()
                .ForMember(p => p.Number, map => map.MapFrom(p => p.Number))
                .ForMember(p => p.DepartmentId, map => map.MapFrom(p => p.DepartmentId))
                .ReverseMap();

            CreateMap<Course, DetailsCourseViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Number, map => map.MapFrom(p => p.Number))
                .ForMember(p => p.Department, map => map.MapFrom(p => p.Department))
                .ForMember(p => p.Groups, map => map.MapFrom(p => p.Groups))
                .ReverseMap();

            CreateMap<Course, DeleteCourseViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Number, map => map.MapFrom(p => p.Number))
                .ForMember(p => p.Number, map => map.MapFrom(p => p.Number))
                .ForMember(p => p.DepartmentName, map => map.MapFrom(p => p.Department.Name))
                .ReverseMap();

            CreateMap<Course, EditCourseViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Number, map => map.MapFrom(p => p.Number))
                .ForMember(p => p.DepartmentName, map => map.MapFrom(p => p.Department.Name))
                .ReverseMap();

            //Department
            CreateMap<Department, CreateDepartmentViewModel>()
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ReverseMap();

            CreateMap<Department, DetailsDepartmentViewModel>()
               .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
               .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ForMember(p => p.Courses, map => map.MapFrom(p => p.Courses))
                .ReverseMap();

            CreateMap<Department, DeleteDepartmentViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ReverseMap();

            CreateMap<Department, EditDepartmentViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(p => p.Id))
                .ForMember(p => p.Name, map => map.MapFrom(p => p.Name))
                .ReverseMap();
        }
    }
}
