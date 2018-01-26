using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DiplomaProjectManagement.Data;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(DiplomaProjectManagement.Web.App_Start.Startup))]

namespace DiplomaProjectManagement.Web.App_Start
{
    public class Startup
    {
        private const string Repository = "Repository";
        private const string Service = "Service";

        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);

            // TODO: Should implement when deploy Asp.Net Identity
            ConfigureAuth(app);
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //Register Controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //Register WebApi
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterForSomeDataTierService(builder);

            //Register ASP.NET Identity
            RegisterForAspNetIdentity(app, builder);

            //Repositories
            RegisterPerRequestFor(Repository, builder);
            //Services
            RegisterPerRequestFor(Service, builder);

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterForSomeDataTierService(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest(); // khi tạo biến IUnitOfWork thì gán type UnitOfWork cho nó
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<DiplomaProjectDbContext>().AsSelf().InstancePerRequest();
        }

        private static void RegisterForAspNetIdentity(IAppBuilder app, ContainerBuilder builder)
        {
            // TODO: Should implement when deploy Asp.Net Identity
            throw new NotImplementedException();
        }

        private static void RegisterPerRequestFor(string type, ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Student).Assembly)
                .Where(n => n.Name.EndsWith(type))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(Facility).Assembly)
                .Where(n => n.Name.EndsWith(type))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(Lecturer).Assembly)
                .Where(n => n.Name.EndsWith(type))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(RegistrationTime).Assembly)
                .Where(n => n.Name.EndsWith(type))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DiplomaProjectRepository).Assembly)
                .Where(n => n.Name.EndsWith(type))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DiplomaProjectRegistrationRepository).Assembly)
                .Where(n => n.Name.EndsWith(type))
                .AsImplementedInterfaces().InstancePerRequest();
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}