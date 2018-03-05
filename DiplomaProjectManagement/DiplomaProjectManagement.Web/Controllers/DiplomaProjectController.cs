using AutoMapper;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Infrastructure.Core;
using DiplomaProjectManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DiplomaProjectManagement.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Lecturer)]
    public class DiplomaProjectController : Controller
    {
        private readonly IDiplomaProjectService _diplomaProjectService;

        public DiplomaProjectController(IDiplomaProjectService diplomaProjectService)
        {
            this._diplomaProjectService = diplomaProjectService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var diplomaProjectViewModel = new DiplomaProjectViewModel();
            return View(diplomaProjectViewModel);
        }

        [HttpPost]
        public ActionResult Create(DiplomaProjectViewModel diplomaProjectViewModel)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var diplomaProject = _diplomaProjectService.GetDiplomaProjectById(id);
            var diplomaProjectViewModel = Mapper.Map<DiplomaProjectViewModel>(diplomaProject);
            return View(diplomaProjectViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DiplomaProjectViewModel diplomaProjectViewModel)
        {
            return View();
        }

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

        public JsonResult GetDiplomaProjectPagination(int page, int pageSize, string keyword = null)
        {
            var currentLecturerId = (int)Session["lecturerId"];
            var query = _diplomaProjectService.GetDiplomaProjectsByLecturerId(currentLecturerId, keyword);

            int totalRow = query.Count();

            query = query.OrderByDescending(n => n.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);

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