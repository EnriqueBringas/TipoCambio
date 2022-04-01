using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TipoCambio.Api.Auth.Entity.Entities.Models;
using TipoCambio.Api.Auth.Entity.Entities.v1;
using TipoCambio.Api.Auth.Interface.Interfaces.v1;
using TipoCambio.Api.Core.Models;
using TipoCambio.Api.Core.Utility.Helpers;
using TipoCambio.Api.Core.Utility.Transfer.Request;
using TipoCambio.Api.Core.Utility.Transfer.Response;

namespace TipoCambio.Api.Auth.Service.Host.Controllers.v1
{
    [Route("v{version:apiVersion}/api/[controller]")]
	[Consumes("application/json")]
	[Produces("application/json")]
	[ApiVersion("1.0")]
	[ApiController]
	public class UsersJwtController : BaseController
	{
		private readonly ITbUsersJwtService _service;

		public UsersJwtController(ServiceConfig environment, ITbUsersJwtService service) : base(environment)
		{
			_service = service;
			_status = new Status();
		}

		[HttpPost("Insert")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbUsersJwt>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Insert(RequestWith<TbUsersJwt> request)
		{
			var lresponse = new ResponseWith<TbUsersJwt>();

			try
			{
				Validation(request);

				Hash.SetHash(request.Parameters.StPassword, out string hpass, out string hsalt);

				request.Parameters.StPassword = hpass;
				request.Parameters.StSalt = hsalt;

				lresponse.Result = await _service.Insert(request.Parameters);

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}

		[HttpPost("Update")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbUsersJwt>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Update(RequestWith<TbUsersJwt> request)
		{
			var lresponse = new ResponseWith<TbUsersJwt>();

			try
			{
				Validation(request);

				Hash.SetHash(request.Parameters.StPassword, out string hpass, out string hsalt);

				request.Parameters.StPassword = hpass;
				request.Parameters.StSalt = hsalt;

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
		public async Task<IActionResult> Delete(RequestWith<TbUsersJwt> request)
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
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbUsersJwt[]>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Select(Request request)
		{
			var lresponse = new ResponseWith<TbUsersJwt[]>();

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
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<TbUsersJwt>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> SelectBy(RequestWith<TbUsersJwt> request)
		{
			var lresponse = new ResponseWith<TbUsersJwt>();

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

		[HttpPost("Generate")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseWith<string>))]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
		public async Task<IActionResult> Generate(RequestWith<TbUsersJwt> request)
		{
			var lresponse = new ResponseWith<string>();

			try
			{
				Validation(request);

				var levaluacion = await _service.IsValidToken(request.Parameters);

				if (levaluacion != null)
				{
					if (Hash.CheckHash(request.Parameters.StPassword, levaluacion.StPassword, levaluacion.StSalt))
					{
						var lkey = Encoding.UTF8.GetBytes(_environment.SecretKey);

						var lclaims = new ClaimsIdentity();
						lclaims.AddClaim(new Claim("user", levaluacion.StCodUserJwt));
						lclaims.AddClaim(new Claim("actived", levaluacion.BlIsActive ? "S" : "N"));

						var tokenDescriptor = new SecurityTokenDescriptor
						{
							Subject = lclaims,
							NotBefore = DateTime.UtcNow,
							Expires = DateTime.UtcNow.AddMinutes(_environment.MinutesToExpires),
							SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(lkey), SecurityAlgorithms.HmacSha256Signature)
						};

						var tokenHandler = new JwtSecurityTokenHandler();
						var createdToken = tokenHandler.CreateToken(tokenDescriptor);
						var bearer_token = tokenHandler.WriteToken(createdToken);

						lresponse.Result = bearer_token;
					}
					else
						_status.AddMessage("Acceso restringido", true);
				}
				else
					_status.AddMessage("Acceso restringido", true);

				return ReturnResponse(HttpStatusCode.OK, request, lresponse);
			}
			catch (Exception ex)
			{
				return ReturnBadRequest(ex, request, lresponse);
			}
		}
	}
}