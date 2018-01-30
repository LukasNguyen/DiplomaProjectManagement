using DiplomaProjectManagement.Model.Entities;
using DiplomaProjectManagement.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DiplomaProjectManagement.Web.Infrastructure
{
    public class ApiControllerBase : ApiController
    {
        private readonly IErrorService _errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            _errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage,
            Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
            }
            catch (DbUpdateException e)
            {
                LogError(e);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, e.InnerException.Message);
            }
            catch (Exception e)
            {
                LogError(e);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error
                {
                    CreatedDate = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
                _errorService.CreateError(error);
                _errorService.Save();
            }
            catch
            {
                // Called when log error not working properly.
            }
        }
    }
}