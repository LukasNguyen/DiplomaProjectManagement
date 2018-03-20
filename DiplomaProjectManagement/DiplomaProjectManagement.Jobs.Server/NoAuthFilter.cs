using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace DiplomaProjectManagement.Jobs.Server
{
    public class NoAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}