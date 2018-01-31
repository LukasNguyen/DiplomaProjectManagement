using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DiplomaProjectManagement.Data;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Service;
using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DiplomaProjectManagement.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;

[assembly: OwinStartup(typeof(DiplomaProjectManagement.Web.App_Start.Startup))]

namespace DiplomaProjectManagement.Web.App_Start
{
    public partial class Startup
    {
        private const string Repository = "Repository";
        private const string Service = "Service";

        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);
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
            RegisterPerRequestForRepository(builder);
            //Services
            RegisterPerRequestForService(builder);

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
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(n => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();
            builder.Register(n => app.GetDataProtectionProvider()).InstancePerRequest();
        }

        private static void RegisterPerRequestForRepository(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(StudentRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(FacilityRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(LecturerRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(RegistrationTimeRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DiplomaProjectRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DiplomaProjectRegistrationRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(ErrorRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();
        }

        private static void RegisterPerRequestForService(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(StudentService).Assembly)
                .Where(n => n.Name.EndsWith(Service))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(FacilityService).Assembly)
                .Where(n => n.Name.EndsWith(Service))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(LecturerService).Assembly)
                .Where(n => n.Name.EndsWith(Service))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(RegistrationTimeService).Assembly)
                .Where(n => n.Name.EndsWith(Service))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DiplomaProjectService).Assembly)
                .Where(n => n.Name.EndsWith(Service))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DiplomaProjectRegistrationService).Assembly)
                .Where(n => n.Name.EndsWith(Service))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(ErrorService).Assembly)
                .Where(n => n.Name.EndsWith(Service))
                .AsImplementedInterfaces().InstancePerRequest();
        }
    }
}