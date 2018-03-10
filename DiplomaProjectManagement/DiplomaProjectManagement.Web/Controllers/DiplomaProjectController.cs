using AutoMapper;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Infrastructure.Core;
using DiplomaProjectManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DiplomaProjectManagement.Web.Infrastructure.Extensions;

namespace DiplomaProjectManagement.Web.Controllers
{
    public class DiplomaProjectController : Controller
    {
        private readonly IDiplomaProjectService _diplomaProjectService;

        public DiplomaProjectController(IDiplomaProjectService diplomaProjectService)
        {
            this._diplomaProjectService = diplomaProjectService;
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpGet]
        public ActionResult Create()
        {
            var diplomaProjectViewModel = new DiplomaProjectViewModel();
            return View(diplomaProjectViewModel);
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpPost]
        public ActionResult Create(DiplomaProjectViewModel diplomaProjectViewModel)
        {
            if (!ModelState.IsValid)
            {
                this.AddErrorMessageToModelState(ModelState);
                return View(diplomaProjectViewModel);
            }

            CreateDiplomaProject();
            this.PrepareSuccessMessage("Thêm đề tài tốt nghiệp thành công");

            return RedirectToAction("Index");

            void CreateDiplomaProject()
            {
                diplomaProjectViewModel.LecturerId = (int)Session["lecturerId"];

                var diplomaProject = Mapper.Map<DiplomaProject>(diplomaProjectViewModel);
                AssignValueBeforeCreateDiplomaProject(diplomaProject);

                _diplomaProjectService.AddDiplomaProject(diplomaProject);
                _diplomaProjectService.Save();
            }

            void AssignValueBeforeCreateDiplomaProject(DiplomaProject diplomaProject)
            {
                diplomaProject.LecturerId = (int)Session["lecturerId"];
                diplomaProject.CreatedDate = DateTime.Now;
                diplomaProject.CreatedBy = User.Identity.Name;
                diplomaProject.Status = true;
            }
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var diplomaProject = _diplomaProjectService.GetDiplomaProjectById(id);

            var diplomaProjectViewModel = Mapper.Map<DiplomaProjectViewModel>(diplomaProject);
            return View(diplomaProjectViewModel);
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpPost]
        public ActionResult Edit(DiplomaProjectViewModel diplomaProjectViewModel)
        {
            if (!ModelState.IsValid)
            {
                this.AddErrorMessageToModelState(ModelState);
                return View(diplomaProjectViewModel);
            }

            EditDiplomaProject();
            this.PrepareSuccessMessage("Sửa đề tài tốt nghiệp thành công");

            return RedirectToAction("Index");

            void EditDiplomaProject()
            {
                var diplomaProject = Mapper.Map<DiplomaProject>(diplomaProjectViewModel);
                AssignValueBeforeUpdateDiplomaProject(diplomaProject);

                _diplomaProjectService.UpdateDiplomaProject(diplomaProject);
                _diplomaProjectService.Save();
            }

            void AssignValueBeforeUpdateDiplomaProject(DiplomaProject diplomaProject)
            {
                diplomaProject.LecturerId = (int)Session["lecturerId"];
                diplomaProject.UpdatedDate = DateTime.Now;
                diplomaProject.UpdatedBy = User.Identity.Name;
                diplomaProject.Status = true;
            }
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var deletedDiplomaProject = _diplomaProjectService.DeleteDiplomaProjectByModifyStatus(id);
            if (deletedDiplomaProject == null)
            {
                return Json(new { status = false });
            }

            _diplomaProjectService.Save();
            return Json(new { status = true });
        }

        [Authorize(Roles = RoleConstants.Student)]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public JsonResult GetDiplomaProjectPagination(int page, int pageSize, string keyword = null)
        {
            var currentLecturerId = (int)Session["lecturerId"];
            var query = _diplomaProjectService.GetDiplomaProjectsByLecturerId(currentLecturerId, keyword);

            int totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var responseData = Mapper.Map<List<DiplomaProjectViewModel>>(query);

            var paginationSet = new PaginationSet<DiplomaProjectViewModel>()
            {
                Items = responseData,
                TotalCount = responseData.Count,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
            };

            return Json(new { data = paginationSet }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiplomaProjectToRegisterPagination(int page, int pageSize, string keyword = null)
        {
            var currentStudentId = (int)Session["studentId"];
            var query = _diplomaProjectService.GetDiplomaProjectsToRegister(currentStudentId, keyword);

            int totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var responseData = Mapper.Map<List<DiplomaProjectViewModel>>(query);

            var paginationSet = new PaginationSet<DiplomaProjectViewModel>()
            {
                Items = responseData,
                TotalCount = responseData.Count,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
            };

            return Json(new { data = paginationSet }, JsonRequestBehavior.AllowGet);
        }
    }
}