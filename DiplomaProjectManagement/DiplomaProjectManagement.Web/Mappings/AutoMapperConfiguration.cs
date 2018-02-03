using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(RegisterMappingForStudent);
        }

        private static void RegisterMappingForStudent(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Student, StudentViewModel>();
        }
    }
}