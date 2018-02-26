using Hangfire;
using System.Configuration;
using System.Web;

namespace DiplomaProjectManagement.Jobs.Server
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            HangfireBootstrapper.Instance.Start();

            RecurringJob.AddOrUpdate<UpdateRegistrationTimeStatusService>(
                "update-registration-time-status",
                x => x.Run(),
                ConfigurationManager.AppSettings["update-registration-time-status:CronExpression"]);
        }

        protected void Application_End()
        {
            HangfireBootstrapper.Instance.Stop();
        }
    }
}