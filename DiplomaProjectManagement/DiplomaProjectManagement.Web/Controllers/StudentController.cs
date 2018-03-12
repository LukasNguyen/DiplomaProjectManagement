using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Service;
using AutoMapper;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Infrastructure.Extensions;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Student)]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDiplomaProjectRegistrationService _diplomaProjectRegistrationService;

        public StudentController(
            IStudentService studentService,
            IDiplomaProjectRegistrationService diplomaProjectRegistrationService)
        {
            _studentService = studentService;
            _diplomaProjectRegistrationService = diplomaProjectRegistrationService;
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

        public ActionResult DiplomaProjectDetails()
        {
            var currentStudentId = (int)Session["studentId"];
            var model = _diplomaProjectRegistrationService.GetDiplomaProjectDetailByStudentId(currentStudentId);
            return View(model);
        }
    }
}