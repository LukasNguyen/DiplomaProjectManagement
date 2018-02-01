using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DiplomaProjectManagement.Web.App_Start;
using Microsoft.AspNet.Identity.Owin;

namespace DiplomaProjectManagement.Web.Api
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
