using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Backend.Entity.Entities.v1;
using TipoCambio.Api.Backend.Entity.Models;
using TipoCambio.Api.Backend.Interface.Interfaces.v1;
using TipoCambio.Api.Core.Models;
using TipoCambio.Api.Core.Utility.Transfer.Request;
using TipoCambio.Api.Core.Utility.Transfer.Response;

namespace TipoCambio.Api.Backend.Service.Host.Controllers.v1
{
    [Route("v{version:apiVersion}/api/[controller]")]
	[Consumes("application/json")]
	[Produces("application/json")]
	[ApiVersion("1.0")]
	[ApiController]
	public class ExchangeRateController : BaseController
	{
		private readonly ITbExchangeRateService _service;

		public ExchangeRateController(ServiceConfig environment, ITbExchangeRateService service) : base(environment)
		{
			_service = service;
			_status = new Status();
		}

		[HttpPost("Insert")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbExchangeRate>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Insert(RequestWith<TbExchangeRate> request)
		{
			var lresponse = new ResponseWith<TbExchangeRate>();

			try
			{
				Validation(request);

				lresponse.Result = await _service.Insert(request.Parameters);

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}

		[HttpPost("Update")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbExchangeRate>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Update(RequestWith<TbExchangeRate> request)
		{
			var lresponse = new ResponseWith<TbExchangeRate>();

			try
			{
				Validation(request);

				lresponse.Result = await _service.Update(request.Parameters);

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}

		[HttpPost("Delete")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<bool>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Delete(RequestWith<TbExchangeRate> request)
		{
			var lresponse = new ResponseWith<bool>();

			try
			{
				Validation(request);

				lresponse.Result = await _service.Delete(request.Parameters);

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}

		[HttpGet("Select")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbExchangeRate[]>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Select(Request request)
		{
			var lresponse = new ResponseWith<TbExchangeRate[]>();

			try
			{
				Validation(request);

				lresponse.Result = await _service.Select();

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}

		[HttpGet("SelectBy")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbExchangeRate>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> SelectBy(RequestWith<TbExchangeRate> request)
		{
			var lresponse = new ResponseWith<TbExchangeRate>();

			try
			{
				Validation(request);

				lresponse.Result = await _service.SelectBy(request.Parameters);

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}

		[HttpGet("ChangeMoney")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<ChangeMoneyRS>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> ChangeMoney(RequestWith<ChangeMoneyRQ> request)
		{
			var lresponse = new ResponseWith<ChangeMoneyRS>();

			try
			{
				Validation(request);

				lresponse.Result = await _service.ChangeMoney(request.Parameters);

				if (lresponse.Result == null)
					_status.AddMessage("No se puede realizar la operación", true);

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}
	}
}