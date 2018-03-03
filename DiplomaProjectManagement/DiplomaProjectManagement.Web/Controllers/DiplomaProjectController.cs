using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Infrastructure.Core;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Controllers
{
    public class DiplomaProjectController : Controller
    {
        private readonly IDiplomaProjectService _diplomaProjectService;

        public DiplomaProjectController(IDiplomaProjectService diplomaProjectService)
        {
            this._diplomaProjectService = diplomaProjectService;
        }
        // GET: DiplomaProject
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return RedirectToAction("Index");
        }

        public JsonResult GetDiplomaProjectPagination(int page, int pageSize, string keyword = null)
        {
            var currentLecturerId = (int)Session["lecturerId"];
            var query = _diplomaProjectService.GetDiplomaProjectsByLecturerId(currentLecturerId);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(n => n.Name.Contains(keyword));
            }

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