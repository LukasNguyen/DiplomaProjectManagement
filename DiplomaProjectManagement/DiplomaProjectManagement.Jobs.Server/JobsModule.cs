using Autofac;
using DiplomaProjectManagement.Data;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;

namespace DiplomaProjectManagement.Jobs.Server
{
    public class JobsModule : Module
    {
        private const string Repository = "Repository";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UpdateRegistrationTimeStatusService>().AsSelf().SingleInstance();
            RegisterForInfrastructureModelAtDataLayer(builder);
            RegisterPerRequestForRepository(builder);
        }

        private void RegisterForInfrastructureModelAtDataLayer(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerLifetimeScope();

            builder.RegisterType<DiplomaProjectDbContext>().AsSelf();
        }

        private void RegisterPerRequestForRepository(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(RegistrationTimeRepository).Assembly)
                .Where(n => n.Name.EndsWith(Repository))
                .AsImplementedInterfaces();
        }
    }
}