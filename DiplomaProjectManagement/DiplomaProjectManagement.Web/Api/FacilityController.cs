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
using System.Web.Script.Serialization;

namespace DiplomaProjectManagement.Web.Api
{
    [RoutePrefix("api/facilities")]
    [Authorize]
    public class FacilityController : ApiControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IErrorService errorService, IFacilityService facilityService) : base(errorService)
        {
            _facilityService = facilityService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var model = _facilityService.GetAllFacilities(keyword);

                totalRow = model.Count();

                var query = model.OrderBy(n => n.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<List<FacilityViewModel>>(query);

                var paginationSet = new PaginationSet<FacilityViewModel>()
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
                    Facility facility = _facilityService.DeleteFacilityByModifyStatus(id);
                    _facilityService.Save();

                    var responseData = Mapper.Map<FacilityViewModel>(facility);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedFacilities)
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
                    var facilities = new JavaScriptSerializer().Deserialize<List<int>>(checkedFacilities);

                    foreach (var facility in facilities)
                    {
                        _facilityService.DeleteFacilityByModifyStatus(facility);
                    }
                    _facilityService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, facilities.Count);
                }
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _facilityService.GetFacilityById(id);

                var responseData = Mapper.Map<Facility, FacilityViewModel>(model);

                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, FacilityViewModel facilityViewModel)
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
                    Facility newFacility = Mapper.Map<Facility>(facilityViewModel);

                    newFacility.CreatedDate = DateTime.Now;
                    newFacility.CreatedBy = User.Identity.Name;

                    Facility facility = _facilityService.AddFacility(newFacility);

                    _facilityService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, facility);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, FacilityViewModel facilityViewModel)
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
                    Facility facilityUpdated = Mapper.Map<Facility>(facilityViewModel);
                    facilityUpdated.UpdatedDate = DateTime.Now;
                    facilityUpdated.UpdatedBy = User.Identity.Name;

                    _facilityService.UpdateFacility(facilityUpdated);
                    _facilityService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, facilityUpdated);
                }

                return response;
            });
        }
    }
}