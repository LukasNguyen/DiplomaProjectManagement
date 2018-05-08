using AutoMapper;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.App_Start;
using DiplomaProjectManagement.Web.Infrastructure.Core;
using DiplomaProjectManagement.Web.Infrastructure.Extensions;
using DiplomaProjectManagement.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace DiplomaProjectManagement.Web.Api
{
    [RoutePrefix("api/lecturers")]
    [Authorize]
    public class LecturerController : ApiControllerBase
    {
        private readonly ILecturerService _lecturerService;
        private ApplicationUserManager _applicationUserManager;

        public LecturerController(IErrorService errorService, ApplicationUserManager applicationUserManager, ILecturerService lecturerService) : base(errorService)
        {
            this._lecturerService = lecturerService;
            this._applicationUserManager = applicationUserManager;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _lecturerService.GetLecturerById(id);

                var responseData = Mapper.Map<LecturerViewModel>(model);

                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var model = _lecturerService.GetAllLecturers(true, keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(n => n.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<List<LecturerViewModel>>(query);

                var paginationSet = new PaginationSet<LecturerViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = responseData.Count,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                return request.CreateResponse(HttpStatusCode.OK, paginationSet);
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                Lecturer lecturer = _lecturerService.DeleteLecturerByModifyStatus(id);
                _lecturerService.Save();

                var responseData = Mapper.Map<LecturerViewModel>(lecturer);
                return request.CreateResponse(HttpStatusCode.Created, responseData);
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedLecturers)
        {
            return CreateHttpResponse(request, () =>
            {
                var lecturers = new JavaScriptSerializer().Deserialize<List<int>>(checkedLecturers);

                foreach (var lecturer in lecturers)
                {
                    _lecturerService.DeleteLecturerByModifyStatus(lecturer);
                }
                _lecturerService.Save();

                return request.CreateResponse(HttpStatusCode.OK, lecturers.Count);
            });
        }

        [Route("create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(HttpRequestMessage request, LecturerLoginViewModel lecturerLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var existingUser = await UserManager.FindByEmailAsync(lecturerLoginViewModel.Email);
            if (existingUser != null)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email này đã tồn tại trong hệ thống. Vui lòng chọn email khác.");
            }

            Lecturer lecturer = CreateLecturerInformation();
            await CreateLecturerAccount();
            _lecturerService.Save();

            return request.CreateResponse(HttpStatusCode.Created, lecturerLoginViewModel);

            Lecturer CreateLecturerInformation()
            {
                var newLecturer = new Lecturer();
                newLecturer.Update(lecturerLoginViewModel);
                return _lecturerService.AddLecturer(newLecturer);
            }

            async Task CreateLecturerAccount()
            {
                var user = new ApplicationUser()
                {
                    UserName = lecturer.Email,
                    Email = lecturer.Email
                };
                await UserManager.CreateAsync(user, lecturerLoginViewModel.Password);
                await UserManager.AddToRoleAsync(user.Id, RoleConstants.Lecturer);
            }
        }

        [Route("update")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(HttpRequestMessage request, LecturerLoginViewModel lecturerLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingLecturer = _lecturerService.GetLecturerById(lecturerLoginViewModel.ID);
                var user = await UserManager.FindByEmailAsync(existingLecturer.Email);

                var existingUser = await UserManager.FindByEmailAsync(lecturerLoginViewModel.Email);
                if (CheckExistingAccount(existingLecturer, existingUser))
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email này đã tồn tại trong hệ thống. Vui lòng chọn email khác.");
                }

                await UpdateLecturerAccount(user);
                await UpdateWhenExistingNewPassword(user);
                UpdateLecturerInformation(existingLecturer);

                _lecturerService.Save();

                return request.CreateResponse(HttpStatusCode.Created, lecturerLoginViewModel);
            }

            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            bool CheckExistingAccount(Lecturer existingLecturer, ApplicationUser existingUser)
            {
                return existingUser != null && existingLecturer != null && !string.Equals(existingUser.Email, existingLecturer.Email, StringComparison.OrdinalIgnoreCase);
            }

            async Task UpdateLecturerAccount(ApplicationUser user)
            {
                user.UserName = lecturerLoginViewModel.Email;
                user.Email = lecturerLoginViewModel.Email;
                await UserManager.UpdateAsync(user);
            }

            async Task UpdateWhenExistingNewPassword(ApplicationUser user)
            {
                if (!string.IsNullOrWhiteSpace(lecturerLoginViewModel.Password))
                {
                    await UserManager.RemovePasswordAsync(user.Id);
                    await UserManager.AddPasswordAsync(user.Id, lecturerLoginViewModel.Password);
                }
            }

            void UpdateLecturerInformation(Lecturer existingLecturer)
            {
                existingLecturer.Update(lecturerLoginViewModel);
            }
        }

        public ApplicationUserManager UserManager
        {
            get => _applicationUserManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _applicationUserManager = value;
        }
    }
}