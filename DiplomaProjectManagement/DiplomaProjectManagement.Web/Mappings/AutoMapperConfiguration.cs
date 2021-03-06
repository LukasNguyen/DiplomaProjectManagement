﻿using AutoMapper;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                RegisterMappingForStudent(cfg);
                RegisterMappingForFacility(cfg);
                RegisterMappingForRegistrationTime(cfg);
                RegisterMappingForLecturer(cfg);
                RegisterMappingForDiplomaProject(cfg);
                RegisterMappingForDiplomaProjectRegistration(cfg);
            });
        }

        private static void RegisterMappingForStudent(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Student, StudentViewModel>().ReverseMap();
        }

        private static void RegisterMappingForFacility(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Facility, FacilityViewModel>().ReverseMap();
        }

        private static void RegisterMappingForRegistrationTime(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<RegistrationTime, RegistrationTimeViewModel>().ReverseMap();
        }

        private static void RegisterMappingForLecturer(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Lecturer, LecturerViewModel>().ReverseMap();
        }

        private static void RegisterMappingForDiplomaProject(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DiplomaProject, DiplomaProjectViewModel>()
                .ForMember(dest => dest.LecturerName, opt => opt.MapFrom(p => p.Lecturer.Name))
                .ForMember(dest => dest.Lecturer, opt => opt.Ignore());

            cfg.CreateMap<DiplomaProjectViewModel, DiplomaProject>();
        }

        private static void RegisterMappingForDiplomaProjectRegistration(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DiplomaProjectRegistration, DiplomaProjectRegistrationViewModel>().ReverseMap();
        }

    }
}