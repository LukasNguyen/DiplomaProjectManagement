using AutoMapper;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Infrastructure.Core;
using DiplomaProjectManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Web.App_Start;
using DiplomaProjectManagement.Web.Infrastructure.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DiplomaProjectManagement.Web.Api
{
    [RoutePrefix("api/students")]
    [Authorize]
    public class StudentController : ApiControllerBase
    {
        private readonly IStudentService _studentService;
        private ApplicationUserManager _applicationUserManager;

        public StudentController(IErrorService errorService, ApplicationUserManager applicationUserManager, IStudentService studentService) : base(errorService)
        {
            this._studentService = studentService;
            this._applicationUserManager = applicationUserManager;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _studentService.GetStudentById(id);

                var responseData = Mapper.Map<Student, StudentViewModel>(model);

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

                var model = _studentService.GetAllStudents(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(n => n.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<List<StudentViewModel>>(query);

                var paginationSet = new PaginationSet<StudentViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
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
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Student student = _studentService.DeleteStudentByModifyStatus(id);
                    _studentService.Save();

                    var responseData = Mapper.Map<Student, StudentViewModel>(student);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedStudents)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var students = new JavaScriptSerializer().Deserialize<List<int>>(checkedStudents);

                    foreach (var item in students)
                    {
                        _studentService.DeleteStudentByModifyStatus(item);
                    }
                    _studentService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, students.Count);
                }
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, StudentLoginViewModel studentLoginViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    // Add new student
                    var student = new Student();
                    student.Update(studentLoginViewModel);
                    student.CreatedBy(User.Identity.Name);
                    var newStudent = _studentService.AddStudent(student);

                    // Add new student's account
                    var user = new ApplicationUser()
                    {
                        UserName = student.Email,
                        Email = student.Email
                    };
                    UserManager.Create(user, studentLoginViewModel.Password);
                    UserManager.AddToRole(user.Id, RoleConstants.Student);

                    _studentService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, studentLoginViewModel);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(HttpRequestMessage request, StudentLoginViewModel studentLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var dbStudent = _studentService.GetStudentById(studentLoginViewModel.ID);

                var user = await UserManager.FindByEmailAsync(dbStudent.Email);
                if (!string.IsNullOrWhiteSpace(studentLoginViewModel.Password))
                {
                    user.Email = studentLoginViewModel.Email;
                    user.UserName = studentLoginViewModel.Email;

                    await UserManager.UpdateAsync(user);
                    await UserManager.RemovePasswordAsync(user.Id);
                    await UserManager.AddPasswordAsync(user.Id, studentLoginViewModel.Password);
                }

                dbStudent.Update(studentLoginViewModel);


                dbStudent.UpdatedBy(User.Identity.Name);

                _studentService.Save();

                return request.CreateResponse(HttpStatusCode.Created, studentLoginViewModel);
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _applicationUserManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _applicationUserManager = value;
            }
        }

    }
}