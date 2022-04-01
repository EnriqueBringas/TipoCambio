using System;
using System.Linq;
using System.Threading.Tasks;
using TipoCambio.Api.Auth.Data.Data.v1;
using TipoCambio.Api.Auth.Entity.Entities.Models;
using TipoCambio.Api.Auth.Entity.Entities.v1;

namespace TipoCambio.Api.Auth.Business.Business.v1
{
    public class TbUsersJwtBL
	{
		protected ServiceConfig _config;

		public TbUsersJwtBL(ServiceConfig environment)
		{
			_config = environment;
		}

		public async Task<TbUsersJwt> Insert(TbUsersJwt objInput)
		{
			TbUsersJwt lrespuesta = null;

			var ldata = await Task.FromResult(new TbUsersJwtDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lusuario = ldata.ToList().Find(c => c.StUserName.Equals(objInput.StUserName));

				if (lusuario != null && !string.IsNullOrEmpty(lusuario.StCodUserJwt))
					throw new Exception("Usuario duplicado");
			}

			objInput.StCodUserJwt = DateTime.Now.ToString("yyyyMMddHHmmssff");

			lrespuesta = await Task.FromResult(new TbUsersJwtDO(_config).Insert(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<TbUsersJwt> Update(TbUsersJwt objInput)
		{
			TbUsersJwt lrespuesta = null;

			var ldata = await Task.FromResult(new TbUsersJwtDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lusuario = ldata.ToList().Find(c => c.StCodUserJwt.Equals(objInput.StCodUserJwt));

				if (lusuario == null)
					throw new Exception("Usuario no existe");
			}

			lrespuesta = await Task.FromResult(new TbUsersJwtDO(_config).Update(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<bool> Delete(TbUsersJwt objInput)
		{
			bool lrespuesta;

			var ldata = await Task.FromResult(new TbUsersJwtDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lusuario = ldata.ToList().Find(c => c.StCodUserJwt.Equals(objInput.StCodUserJwt));

				if (lusuario == null)
					throw new Exception("Usuario no existe");
			}

			lrespuesta = await Task.FromResult(new TbUsersJwtDO(_config).Delete(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<TbUsersJwt[]> Select()
		{
			return await Task.FromResult(new TbUsersJwtDO(_config).Select()).ConfigureAwait(false);
		}

		public async Task<TbUsersJwt> SelectBy(TbUsersJwt objInput)
		{
			return await Task.FromResult(new TbUsersJwtDO(_config).SelectBy(objInput)).ConfigureAwait(false);
		}

		public async Task<TbUsersJwt> IsValidToken(TbUsersJwt objInput)
		{
			TbUsersJwt lrespuesta = null;

			var ldata = await Task.FromResult(new TbUsersJwtDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
            {
				var lusuario = ldata.ToList().Find(c => c.StUserName.Equals(objInput.StUserName));

				if (lusuario != null)
					lrespuesta = lusuario;
			}

			return lrespuesta;
		}
	}
}