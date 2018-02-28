using System.Configuration;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Hangfire;
using System.Web.Hosting;

namespace DiplomaProjectManagement.Jobs.Server
{
    public class HangfireBootstrapper : IRegisteredObject
    {
        private readonly object _lockObject = new object();
        private bool _started;
        private BackgroundJobServer _backgroundJobServer;

        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

        private HangfireBootstrapper()
        {
            // Singleton pattern, please do NOT remove this empty constructor.
        }

        public void Start()
        {
            lock (_lockObject)
            {
                if (_started)
                {
                    return;
                }

                _started = true;

                HostingEnvironment.RegisterObject(this);
                ConfigureHangfireStorage();
                ConfigureHangfireAutofac();

                _backgroundJobServer = new BackgroundJobServer();
            }
        }

        private void ConfigureHangfireStorage()
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(
                ConfigurationManager
                .ConnectionStrings["DiplomaProjectConnection"]
                .ConnectionString);
        }

        private void ConfigureHangfireAutofac()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new JobsModule());

            var container = containerBuilder.Build();
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        public void Stop()
        {
            lock (_lockObject)
            {
                _backgroundJobServer?.Dispose();
                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}