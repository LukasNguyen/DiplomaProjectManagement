using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DiplomaProjectManagement.Jobs.Server.App_Start.Startup))]

namespace DiplomaProjectManagement.Jobs.Server.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard("");
        }
    }
}