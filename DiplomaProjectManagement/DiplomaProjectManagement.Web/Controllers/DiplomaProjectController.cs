﻿using AutoMapper;
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
using DiplomaProjectManagement.Common.CustomViewModel;

namespace DiplomaProjectManagement.Web.Controllers
{
    public class DiplomaProjectController : Controller
    {
        private readonly IDiplomaProjectService _diplomaProjectService;
        private readonly IDiplomaProjectRegistrationService _diplomaProjectRegistrationService;
        private readonly IRegistrationTimeService _registrationTimeService;

        public DiplomaProjectController(
            IDiplomaProjectService diplomaProjectService,
            IDiplomaProjectRegistrationService diplomaProjectRegistrationService,
            IRegistrationTimeService registrationTimeService)
        {
            _diplomaProjectService = diplomaProjectService;
            _diplomaProjectRegistrationService = diplomaProjectRegistrationService;
            _registrationTimeService = registrationTimeService;
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpGet]
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

        [Authorize(Roles = RoleConstants.Student)]
        [HttpPost]
        public ActionResult Register(int id)
        {
            var studentId = (int)Session["studentId"];
            var registrationTimeId = GetActiveRegisterTimeId();
            var numberOfStudentRegistered = _diplomaProjectRegistrationService
                .GetNumberOfStudentRegistered(id, registrationTimeId);

            if (numberOfStudentRegistered == GetLimitNumberOfStudentRegistered())
            {
                return Json(new { status = 2 });
            }

            var diplomaProjectRegistrationViewModel = CreateDiplomaProjectRegistrationViewModel();

            var dipmaProjectRegistration = Mapper.Map<DiplomaProjectRegistration>(diplomaProjectRegistrationViewModel);
            _diplomaProjectRegistrationService.AddDiplomaProjectRegistration(dipmaProjectRegistration);

            try
            {
                _diplomaProjectRegistrationService.Save();
                return Json(new { status = 0 });
            }
            catch
            {
                return Json(new { status = 1 });
            }

            DiplomaProjectRegistrationViewModel CreateDiplomaProjectRegistrationViewModel()
            {
                return new DiplomaProjectRegistrationViewModel
                {
                    DiplomaProjectId = id,
                    StudentId = studentId,
                    RegistrationTimeId = GetActiveRegisterTimeId(),
                    IsFirstStudentInTeamRegistered = true
                };
            }

            int GetActiveRegisterTimeId()
            {
                return _registrationTimeService.GetActiveRegisterTimeId();
            }

            int GetLimitNumberOfStudentRegistered()
            {
                return int.Parse(ConfigHelper.GetByKey("LimitNumberOfStudentRegistered"));
            }
        }

        [Authorize(Roles = RoleConstants.Lecturer)]
        [HttpGet]
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

        [Authorize(Roles = RoleConstants.Student)]
        [HttpGet]
        public JsonResult GetDiplomaProjectToRegisterPagination(int page, int pageSize, string keyword = null)
        {
            var currentStudentId = (int)Session["studentId"];

            if (CheckStudentAlreadyHaveGotFinalGrades())
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }

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

            return Json(new { data = paginationSet, status = true }, JsonRequestBehavior.AllowGet);

            bool CheckStudentAlreadyHaveGotFinalGrades()
            {
                var diplomaProjectRegistration = _diplomaProjectRegistrationService.GetDiplomaProjectDetailByStudentId(currentStudentId);

                if (StudentAlreadyHaveIntroduceGradesAndReviewGrades(diplomaProjectRegistration))
                {
                    var finalGrades = CalculateFinalGrades(diplomaProjectRegistration);

                    if (finalGrades >= 5)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }

            bool StudentAlreadyHaveIntroduceGradesAndReviewGrades(DiplomaProjectDetailViewModel diplomaProjectRegistration)
            {
                return diplomaProjectRegistration != null && diplomaProjectRegistration.IntroducedGrades != null && diplomaProjectRegistration.ReviewedGrades != null;
            }

            double? CalculateFinalGrades(DiplomaProjectDetailViewModel diplomaProjectRegistration)
            {
                return diplomaProjectRegistration.IntroducedGrades * 0.6 + diplomaProjectRegistration.ReviewedGrades * 0.4;
            }
        }

        [Authorize(Roles = RoleConstants.Student)]
        [HttpGet]
        public ActionResult DiplomaProjectsRemainingSlot()
        {
            return View();
        }

        [Authorize(Roles = RoleConstants.Student)]
        [HttpGet]
        public JsonResult GetDiplomaProjectsRemainingSlot(int page, int pageSize, string keyword = null)
        {
            var query = _diplomaProjectService.GetDiplomaProjectsRemainingSlot(keyword);

            int totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var paginationSet = new PaginationSet<DiplomaProjectRemainingSlotViewModel>()
            {
                Items = query,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
            };

            return Json(new { data = paginationSet }, JsonRequestBehavior.AllowGet);
        }
    }
}