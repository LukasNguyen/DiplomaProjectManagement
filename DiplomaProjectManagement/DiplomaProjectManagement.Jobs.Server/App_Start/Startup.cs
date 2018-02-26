using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using DiplomaProjectManagement.Data;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DiplomaProjectManagement.Jobs.Server.App_Start.Startup))]

namespace DiplomaProjectManagement.Jobs.Server.App_Start
{
    public class Startup
    {
        private const string Repository = "Repository";

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            RegisterForInfrastructureModelAtDataLayer(builder);
            RegisterPerRequestForRepository(builder);

            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(container));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseHangfireDashboard("");
            app.UseHangfireServer();
        }

        private void RegisterForInfrastructureModelAtDataLayer(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<DiplomaProjectDbContext>().AsSelf().InstancePerRequest();
        }

        private void RegisterPerRequestForRepository(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(RegistrationTimeRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces().InstancePerRequest();
        }
    }
}