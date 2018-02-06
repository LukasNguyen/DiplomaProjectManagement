using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
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
            });
        }

        private static void RegisterMappingForStudent(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Student, StudentViewModel>();
        }

        private static void RegisterMappingForFacility(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Facility, FacilityViewModel>().ReverseMap();
        }
    }
}