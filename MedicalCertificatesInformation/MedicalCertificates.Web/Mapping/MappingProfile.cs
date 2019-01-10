using AutoMapper;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Web.Models.HospitalViewModels;
using MedicalCertificates.Web.Models.PhysicalEducationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
