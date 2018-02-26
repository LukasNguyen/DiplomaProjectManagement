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

                _backgroundJobServer = new BackgroundJobServer();
            }
        }

        private void ConfigureHangfireStorage()
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DiplomaProjectConnection");
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