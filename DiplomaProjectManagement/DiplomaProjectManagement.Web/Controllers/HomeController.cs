using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Service;
using System.Web.Mvc;

namespace DiplomaProjectManagement.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILecturerService _lecturerService;
        private readonly IStudentService _studentService;

        public HomeController(ILecturerService lecturerService, IStudentService studentService)
        {
            _lecturerService = lecturerService;
            _studentService = studentService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
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

        public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
        }
    }