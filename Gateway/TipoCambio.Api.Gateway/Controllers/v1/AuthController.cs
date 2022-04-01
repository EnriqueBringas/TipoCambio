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
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthController(IAuthenticationService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("Generate")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<string>))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
        public async Task<IActionResult> Generate(RequestWith<TokenRQ> request)
        {
            var lresult = await _service.Generate(request);

            return Ok(lresult);
        }
    }
}