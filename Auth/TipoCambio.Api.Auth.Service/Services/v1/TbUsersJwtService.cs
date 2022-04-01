using System.Threading.Tasks;
using TipoCambio.Api.Auth.Business.Business.v1;
using TipoCambio.Api.Auth.Entity.Entities.Models;
using TipoCambio.Api.Auth.Entity.Entities.v1;
using TipoCambio.Api.Auth.Interface.Interfaces.v1;
using TipoCambio.Api.Auth.Service.Validations;
using TipoCambio.Api.Auth.Service.Validations.v1;

namespace TipoCambio.Api.Auth.Service.Services.v1
{
    public class TbUsersJwtService : BaseService, ITbUsersJwtService
	{
		public TbUsersJwtService(ServiceConfig environment)
		{
			_config = environment;
		}

        public async Task<TbUsersJwt> Insert(TbUsersJwt objInput)
        {
            ValidRequest(objInput);

            return await new TbUsersJwtBL(_config).Insert(objInput);
        }

        public async Task<TbUsersJwt> Update(TbUsersJwt objInput)
        {
            ValidRequest(objInput);

            return await new TbUsersJwtBL(_config).Update(objInput);
        }

        public async Task<bool> Delete(TbUsersJwt objInput)
        {
            ValidRequest(objInput);

            return await new TbUsersJwtBL(_config).Delete(objInput);
        }

        public async Task<TbUsersJwt[]> Select()
        {
            return await new TbUsersJwtBL(_config).Select();
        }

        public async Task<TbUsersJwt> SelectBy(TbUsersJwt objInput)
        {
            ValidRequest(objInput);

            return await new TbUsersJwtBL(_config).SelectBy(objInput);
        }

        public async Task<TbUsersJwt> IsValidToken(TbUsersJwt objInput)
        {
            ValidRequest(objInput);

            FluentHelper.ExecuteValidator<TbUsersJwtValidator, TbUsersJwt>(objInput);

            return await new TbUsersJwtBL(_config).IsValidToken(objInput);
        }
    }
}