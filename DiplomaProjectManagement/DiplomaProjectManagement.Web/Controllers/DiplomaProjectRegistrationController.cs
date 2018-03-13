using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using System.Linq;
using System.Web.Mvc;

namespace DiplomaProjectManagement.Web.Controllers
{
    public class DiplomaProjectRegistrationController : Controller
    {
        private readonly IRegistrationTimeService _registrationTimeService;

        public DiplomaProjectRegistrationController(IRegistrationTimeService registrationService)
        {
            _registrationTimeService = registrationService;
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
    }
}