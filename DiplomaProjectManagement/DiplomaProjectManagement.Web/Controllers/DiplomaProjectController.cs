using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Controllers
{
    public class DiplomaProjectController : Controller
    {
        private readonly IDiplomaProjectService _diplomaProjectService;
        private readonly ILecturerService _lecturerService;

        public DiplomaProjectController(IDiplomaProjectService diplomaProjectService, ILecturerService lecturerService)
        {
            this._diplomaProjectService = diplomaProjectService;
            this._lecturerService = lecturerService;
        }
        // GET: DiplomaProject
        public ActionResult Index()
        {
            var currentLecturerId = GetLecturerIdByUserEmail(User.Identity.Name);
            var getDiplomaProjectsActive = _diplomaProjectService.GetDiplomaProjectsByLecturerId(currentLecturerId);

            var diplomaProjectsActive = Mapper.Map<List<DiplomaProjectViewModel>>(getDiplomaProjectsActive);

            return View(diplomaProjectsActive);
        }

        public ActionResult Edit()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return RedirectToAction("Index");
        }

        private int GetLecturerIdByUserEmail(string email)
        {
            return _lecturerService.GetLecturerByEmail(email).ID;
        }
    }
}