using AutoMapper;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Infrastructure.Extensions;
using DiplomaProjectManagement.Web.Models;
using System;
using System.Web.Mvc;

namespace DiplomaProjectManagement.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Student)]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDiplomaProjectRegistrationService _diplomaProjectRegistrationService;
        private readonly IRegistrationTimeService _registrationTimeService;

        public StudentController(
            IStudentService studentService,
            IDiplomaProjectRegistrationService diplomaProjectRegistrationService,
            IRegistrationTimeService registrationTimeService)
        {
            _studentService = studentService;
            _diplomaProjectRegistrationService = diplomaProjectRegistrationService;
            _registrationTimeService = registrationTimeService;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var currentStudentId = (int)Session["studentId"];
            var student = _studentService.GetStudentById(currentStudentId);

            var studentViewModel = Mapper.Map<StudentViewModel>(student);
            return View(studentViewModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                this.AddErrorMessageToModelState(ModelState);
                return View(studentViewModel);
            }

            EditStudent();
            this.PrepareSuccessMessage("Sửa thông tin cá nhân thành công");

            return RedirectToAction("Dashboard", "Home");

            void EditStudent()
            {
                var student = Mapper.Map<Student>(studentViewModel);
                AssignValueBeforeUpdateStudent(student);

                _studentService.UpdateStudent(student);
                _studentService.Save();
            }

            void AssignValueBeforeUpdateStudent(Student student)
            {
                student.ID = (int)Session["studentId"];
                student.UpdatedDate = DateTime.Now;
                student.UpdatedBy = User.Identity.Name;
                student.Status = true;
                student.Email = _studentService.GetStudentEmail(student.ID);
            }
        }

        [HttpGet]
        public ActionResult AddTeamMember()
        {
            var model = new DiplomaProjectTeamRegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTeamMember(DiplomaProjectTeamRegistrationViewModel viewModel)
        {
            var currentStudentId = (int)Session["studentId"];
            var activeRegistrationTimeId = GetActiveRegisterTimeId();
            var partner = _studentService.GetStudentByEmail(viewModel.Email);

            if (activeRegistrationTimeId == 0)
            {
                ModelState.AddModelError("", "Không thể thêm thành viên khi đợt này không còn mở cho sinh viên đăng ký.");
                return View(viewModel);
            }

            if (partner == null)
            {
                ModelState.AddModelError("", "Email sinh viên này không tồn tại trong hệ thống.");
                return View(viewModel);
            }

            if (partner.ID == currentStudentId)
            {
                ModelState.AddModelError("", "Bạn không thể nhập email của chính mình.");
                return View(viewModel);
            }

            var diplomaProjectId = _diplomaProjectRegistrationService
                .FindDiplomaProject(partner.ID, activeRegistrationTimeId);

            if (CheckStudentIsNotRegistered())
            {
                ModelState.AddModelError("", "Bạn chưa đăng ký đề tài nên không thể thêm thành viên.");
                return View(viewModel);
            }

            if (CheckStudentAlreadyHaveGotTeamMember())
            {
                ModelState.AddModelError("", "Bạn đã đăng ký đề tài này với một người trong đợt này.");
                return View(viewModel);
            }

            _diplomaProjectRegistrationService
                .UpdateTeamName(currentStudentId, partner.ID,
                diplomaProjectId, activeRegistrationTimeId,
                viewModel.TeamName);

            _diplomaProjectRegistrationService.Save();

            return RedirectToAction("Dashboard", "Home");

            int GetActiveRegisterTimeId()
            {
                return _registrationTimeService.GetActiveRegisterTimeId();
            }

            bool CheckStudentIsNotRegistered()
            {
                return _diplomaProjectRegistrationService
                           .FindDiplomaProject(partner.ID, activeRegistrationTimeId) == 0;
            }

            bool CheckStudentAlreadyHaveGotTeamMember()
            {
                return !string.IsNullOrWhiteSpace(_diplomaProjectRegistrationService
                    .FindTeamName(currentStudentId, activeRegistrationTimeId));
            }
        }

        public ActionResult DiplomaProjectDetails()
        {
            var currentStudentId = (int)Session["studentId"];
            var model = _diplomaProjectRegistrationService.GetDiplomaProjectDetailByStudentId(currentStudentId);
            return View(model);
        }
    }
}