using AutoMapper;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.App_Start;
using DiplomaProjectManagement.Web.Infrastructure.Core;
using DiplomaProjectManagement.Web.Infrastructure.Extensions;
using DiplomaProjectManagement.Web.Models;
using Microsoft.AspNet.Identity;
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

                    foreach (var student in students)
                    {
                        _studentService.DeleteStudentByModifyStatus(student);
                    }
                    _studentService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, students.Count);
                }
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(HttpRequestMessage request, StudentLoginViewModel studentLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var existingUser = await UserManager.FindByEmailAsync(studentLoginViewModel.Email);
            if (existingUser != null)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "This email already existing in system. Please use another email.");
            }

            Student student = CreateStudentInformation();
            await CreateStudentAccount();
            _studentService.Save();

            return request.CreateResponse(HttpStatusCode.Created, studentLoginViewModel);

            Student CreateStudentInformation()
            {
                var newStudent = new Student();
                newStudent.Update(studentLoginViewModel);
                newStudent.CreatedBy(User.Identity.Name);
                return _studentService.AddStudent(newStudent);
            }

            async Task CreateStudentAccount()
            {
                var user = new ApplicationUser()
                {
                    UserName = student.Email,
                    Email = student.Email
                };
                await UserManager.CreateAsync(user, studentLoginViewModel.Password);
                await UserManager.AddToRoleAsync(user.Id, RoleConstants.Student);
            }
        }

        [Route("update")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(HttpRequestMessage request, StudentLoginViewModel studentLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var dbStudent = _studentService.GetStudentById(studentLoginViewModel.ID);
                var user = await UserManager.FindByEmailAsync(dbStudent.Email);

                var existingUser = await UserManager.FindByEmailAsync(studentLoginViewModel.Email);
                if (CheckExistingAccount(dbStudent, existingUser))
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "This email already existing in system. Please use another email.");
                }

                await UpdateStudentAccount(user);
                await UpdateWhenExistingNewPassword(user);
                UpdateStudentInformation(dbStudent);

                _studentService.Save();

                return request.CreateResponse(HttpStatusCode.Created, studentLoginViewModel);
            }

            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            bool CheckExistingAccount(Student dbStudent, ApplicationUser existingUser)
            {
                return existingUser != null && dbStudent != null && !string.Equals(existingUser.Email, dbStudent.Email, StringComparison.OrdinalIgnoreCase);
            }

            async Task UpdateStudentAccount(ApplicationUser user)
            {
                user.UserName = studentLoginViewModel.Email;
                user.Email = studentLoginViewModel.Email;
                await UserManager.UpdateAsync(user);
            }

            async Task UpdateWhenExistingNewPassword(ApplicationUser user)
            {
                if (!string.IsNullOrWhiteSpace(studentLoginViewModel.Password))
                {
                    await UserManager.RemovePasswordAsync(user.Id);
                    await UserManager.AddPasswordAsync(user.Id, studentLoginViewModel.Password);
                }
            }

            void UpdateStudentInformation(Student dbStudent)
            {
                dbStudent.Update(studentLoginViewModel);
                dbStudent.UpdatedBy(User.Identity.Name);
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