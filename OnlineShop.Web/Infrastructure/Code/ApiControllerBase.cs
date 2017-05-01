using OnlineShop.Model.Models;
using OnlineShop.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShop.Web.Infrastructure.Code
{
    public class ApiControllerBase : ApiController
    {
        public IErrorService _errorService;
        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }
        protected HttpResponseMessage CreateHttpResponseMessage(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage respon = null;
            try
            {
                respon = function.Invoke();
            }
            catch(DbEntityValidationException ex)
            {
                foreach(var exe in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{exe.Entry.Entity.GetType().Name}\" in state \"{exe.Entry.State}\" has the following validation errors :");
                    foreach(var exe1 in exe.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property : \"{exe1.PropertyName}\", Errors :  \"{exe1.ErrorMessage}\"");
                    }
                }
            }
            catch(DbUpdateException ex1)
            {
                LogError(ex1);
                respon = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex1.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                respon = requestMessage.CreateResponse(HttpStatusCode.BadRequest,ex.Message);
            }
            return respon;
        }
        public void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreateDate = DateTime.Now;
                error.Messeage = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Add(error);
                _errorService.SaveChange();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
