using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.App_Start;
using DiplomaProjectManagement.Web.Infrastructure.Extensions;
using DiplomaProjectManagement.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DiplomaProjectManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ILecturerService _lecturerService;
        private readonly IStudentService _studentService;

        public HomeController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ILecturerService lecturerService,
            IStudentService studentService)
        {
            _lecturerService = lecturerService;
            _studentService = studentService;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindAsync(model.UserName, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác.");
                    return View(model);
                }

                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                var props = new AuthenticationProperties();
                props.IsPersistent = model.RememberMe;
                authenticationManager.SignIn(props, identity);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                this.PrepareSuccessMessage("Đăng nhập thành công");
                return RedirectToAction("Dashboard", "Home");
            }

            return View(model);
        }

        [Authorize(Roles = RoleConstants.Lecturer + ", " + RoleConstants.Student)]
        public ActionResult Dashboard()
        {
            AssignCurrentUserIdToSessionAppropriately();
            return View();

            void AssignCurrentUserIdToSessionAppropriately()
            {
                if (User.IsInRole(RoleConstants.Lecturer))
                {
                    var currentLecturerId = _lecturerService.GetLecturerByEmail(User.Identity.Name);
                    Session["lecturerId"] = currentLecturerId.ID;
                }
                else
                {
                    var currentStudentId = _studentService.GetStudentByEmail(User.Identity.Name);
                    Session["studentId"] = currentStudentId.ID;
                }
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}