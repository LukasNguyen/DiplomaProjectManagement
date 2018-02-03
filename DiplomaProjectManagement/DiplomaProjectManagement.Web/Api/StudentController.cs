using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using DiplomaProjectManagement.Service;
using DiplomaProjectManagement.Web.Infrastructure;
using System.Web.Http;
using System.Web.Script.Serialization;
using AutoMapper;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Api
{
    [RoutePrefix("api/students")]
    [Authorize]

    public class StudentController : ApiControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IErrorService errorService,IStudentService studentService) : base(errorService)
        {
            this._studentService = studentService;
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
    }
}
