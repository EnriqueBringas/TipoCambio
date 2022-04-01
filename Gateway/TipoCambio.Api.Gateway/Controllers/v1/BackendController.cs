using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TipoCambio.Api.Core.DTO;
using TipoCambio.Api.Core.Utility.Transfer.Request;
using TipoCambio.Api.Core.Utility.Transfer.Response;
using TipoCambio.Api.Gateway.Interfaces.v1;

namespace TipoCambio.Api.Gateway.Controllers.v1
{
    [Route("v{version:apiVersion}/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    public class BackendController : ControllerBase
    {
        private readonly IBackendService _service;

        public BackendController(IBackendService service)
        {
            _service = service;
        }

        [HttpGet("Currency")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<CurrencyRS[]>))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
        public async Task<IActionResult> Currency(Request request)
        {
            var lresult = await _service.Currency(request);

            return Ok(lresult);
        }

        [HttpGet("ChangeMoney")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<ChangeMoneyRS>))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
        public async Task<IActionResult> ChangeMoney(RequestWith<ChangeMoneyRQ> request)
        {
            var lresult = await _service.ChangeMoney(request);

            return Ok(lresult);
        }

        [HttpPost("InsertExchange")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ExchangeRS))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
        public async Task<IActionResult> InsertExchange(RequestWith<ExchangeRQ> request)
        {
            var lresult = await _service.InsertExchange(request);

            return Ok(lresult);
        }

        [HttpPost("UpdateExchange")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<ExchangeRS>))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
        public async Task<IActionResult> UpdateExchange(RequestWith<ExchangeRQ> request)
        {
            var lresult = await _service.UpdateExchange(request);

            return Ok(lresult);
        }

        [HttpPost("DeleteExchange")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<bool>))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
        public async Task<IActionResult> DeleteExchange(RequestWith<ExchangeRQ> request)
        {
            var lresult = await _service.DeleteExchange(request);

            return Ok(lresult);
        }

        [HttpGet("SelectExchange")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<ExchangeRS[]>))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
        public async Task<IActionResult> SelectExchange(RequestWith<ExchangeRQ> request)
        {
            var lresult = await _service.SelectExchange(request);

            return Ok(lresult);
        }
    }
}