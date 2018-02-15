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
using System.Web.Http;

namespace DiplomaProjectManagement.Web.Api
{
    [RoutePrefix("api/registrationTimes")]
    [Authorize]
    public class RegistrationTimeController : ApiControllerBase
    {
        private readonly IRegistrationTimeService _registrationTimeService;

        public RegistrationTimeController(IErrorService errorService, IRegistrationTimeService registrationTimeService) : base(errorService)
        {
            this._registrationTimeService = registrationTimeService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var model = _registrationTimeService.GetAllRegistrationTimes();

                totalRow = model.Count();
                var query = model.Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<List<RegistrationTimeViewModel>>(query);

                var paginationSet = new PaginationSet<RegistrationTimeViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                return request.CreateResponse(HttpStatusCode.OK, paginationSet);
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _registrationTimeService.GetRegistrationTimeById(id);

                var responseData = Mapper.Map<RegistrationTimeViewModel>(model);

                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, RegistrationTimeViewModel registrationTimeViewModel)
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
                    RegistrationTime newRegistrationTime = Mapper.Map<RegistrationTime>(registrationTimeViewModel);

                    RegistrationTime registrationTime = _registrationTimeService.AddRegistrationTime(newRegistrationTime);
                    _registrationTimeService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, registrationTime);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, RegistrationTimeViewModel registrationTimeViewModel)
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
                    RegistrationTime registrationTimeUpdated = Mapper.Map<RegistrationTime>(registrationTimeViewModel);

                    _registrationTimeService.UpdateRegistrationTime(registrationTimeUpdated);
                    _registrationTimeService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, registrationTimeUpdated);
                }

                return response;
            });
        }
    }
}