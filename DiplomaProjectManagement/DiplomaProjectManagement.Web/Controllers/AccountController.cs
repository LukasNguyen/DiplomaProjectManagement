using System;
using DiplomaProjectManagement.Web.App_Start;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace DiplomaProjectManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [Authorize]
        public ActionResult LogOut()
        {
            try
            {
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut();
                Session.Clear();
                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }
    }
}