using AutoMapper;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Common.CustomViewModel;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DiplomaProjectManagement.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Lecturer)]
    public class DiplomaProjectRegistrationController : Controller
    {
        private readonly IRegistrationTimeService _registrationTimeService;
        private readonly IStudentService _studentService;
        private readonly IDiplomaProjectRegistrationService _diplomaProjectRegistrationService;

        public DiplomaProjectRegistrationController(
            IRegistrationTimeService registrationService,
            IStudentService studentService,
            IDiplomaProjectRegistrationService diplomaProjectRegistrationService)
        {
            _registrationTimeService = registrationService;
            _studentService = studentService;
            _diplomaProjectRegistrationService = diplomaProjectRegistrationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpGet]
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

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpGet]
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
                return Json(new { data = Enumerable.Empty<LecturerAssignGradesViewModel>() },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpPost]
        public ActionResult AssignGrades(string viewModel)
        {
            try
            {
                int countErrorsWhenUpdating = 0;
                List<DiplomaProjectRegistration> diplomaProjectRegistrations = ConvertFromViewModel();

                countErrorsWhenUpdating = UpdateGrades(countErrorsWhenUpdating, diplomaProjectRegistrations);

                if (countErrorsWhenUpdating == 0)
                {
                    _diplomaProjectRegistrationService.Save();
                    return Json(new { status = 0 });
                }

                return Json(new { status = 1 });
            }
            catch (Exception ex) when (BeFormatException(ex))
            {
                return Json(new { status = 2 });
            }
            catch (Exception)
            {
                return Json(new { status = 3 });
            }

            List<DiplomaProjectRegistration> ConvertFromViewModel()
            {
                var diplomaProjectRegistrationsViewModel = new JavaScriptSerializer()
                    .Deserialize<List<DiplomaProjectRegistrationViewModel>>(viewModel);

                return Mapper.Map<List<DiplomaProjectRegistration>>(diplomaProjectRegistrationsViewModel);
            }

            int UpdateGrades(int errorCount, List<DiplomaProjectRegistration> diplomaProjectRegistrations)
            {
                foreach (var diplomaProjectRegistration in diplomaProjectRegistrations)
                {
                    if (CheckValidGrades(diplomaProjectRegistration.IntroducedGrades) && CheckValidGrades(diplomaProjectRegistration.ReviewedGrades))
                    {
                        _diplomaProjectRegistrationService.UpdateGrades(diplomaProjectRegistration);
                        continue;
                    }

                    errorCount++;
                    break;
                }

                return errorCount;
            }

            bool BeFormatException(Exception exception)
            {
                var formatException = exception.InnerException as FormatException;

                return formatException?.Message == "Input string was not in a correct format.";
            }
        }

        private static bool CheckValidGrades(float? grades)
        {
            return grades == null || grades >= 0 && grades <= 10;
        }
    }
}