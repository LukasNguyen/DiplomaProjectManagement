using DiplomaProjectManagement.Web.App_Start;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DiplomaProjectManagement.Web.Api
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [Route("logout")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Logout(HttpRequestMessage request)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }
    }
}