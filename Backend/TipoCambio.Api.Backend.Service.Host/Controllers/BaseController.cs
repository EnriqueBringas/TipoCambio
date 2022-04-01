using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Core.Models;
using TipoCambio.Api.Core.Utility.Transfer.Request;
using TipoCambio.Api.Core.Utility.Transfer.Response;

namespace TipoCambio.Api.Backend.Service.Host.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected Status _status;
        protected readonly ServiceConfig _environment;

        public BaseController(ServiceConfig environment)
        {
            _environment = environment;
        }

        protected void Validation(Request request)
        {
            if (request == null)
                throw new ArgumentException("Request no existe");
            else
            {
                if (string.IsNullOrEmpty(request.Application))
                    throw new ArgumentException("Nombre de la aplicación solicitante no existe", "Application");

                if (string.IsNullOrEmpty(request.Tracking))
                    throw new ArgumentException("Código de seguimiento no existe", "Tracking");
            }
        }

        protected JsonResult ReturnBadRequest(Exception ex, Request request, Response response)
        {
            response.Status = new Status(ex)
            {
                Tracking = request.Tracking
            };

            return new JsonResult(response);
        }

        protected JsonResult ReturnResponse<TResult>(HttpStatusCode httpStatusCode, Request request, TResult response) where TResult : Response
        {
            response.Status = _status;
            response.Status.Ok = response.Status.Messages == null || !response.Status.Messages.Any(c => c.IsError);
            response.Status.Tracking = request.Tracking;

            Response.StatusCode = (int)httpStatusCode;

            return new JsonResult(response);
        }
    }
}