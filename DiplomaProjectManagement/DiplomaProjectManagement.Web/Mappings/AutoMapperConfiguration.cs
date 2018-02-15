using AutoMapper;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Models;
using System;
using DiplomaProjectManagement.Model.Enums;

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
    }
}