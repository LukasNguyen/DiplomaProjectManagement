using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using System.Linq;
using System.Web.Mvc;
using DiplomaProjectManagement.Common.CustomViewModel;

namespace DiplomaProjectManagement.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Lecturer)]
    public class DiplomaProjectRegistrationController : Controller
    {
        private readonly IRegistrationTimeService _registrationTimeService;
        private readonly IStudentService _studentService;

        public DiplomaProjectRegistrationController(
            IRegistrationTimeService registrationService,
            IStudentService studentService)
        {
            _registrationTimeService = registrationService;
            _studentService = studentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRegistrationTimes()
        {
            var registrationTimes = _registrationTimeService.GetAllRegistrationTimes();

            if (registrationTimes.Any())
            {
                return Json(new { data = registrationTimes }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = Enumerable.Empty<RegistrationTime>() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetStudentToAssignGrades(int registrationTimeId)
        {
            var currentLecturerId = (int)Session["lecturerId"];
            var students = _studentService.GetStudentsToAssignGrades(currentLecturerId, registrationTimeId);

            if (students.Any())
            {
                return Json(new { data = students }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = Enumerable.Empty<LecturerAssignGradesViewModel>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}