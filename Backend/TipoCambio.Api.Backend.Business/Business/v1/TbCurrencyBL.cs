using System;
using System.Linq;
using System.Threading.Tasks;
using TipoCambio.Api.Backend.Data.Data.v1;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Backend.Entity.Entities.v1;

namespace TipoCambio.Api.Backend.Business.Business.v1
{
	public class TbCurrencyBL
	{
		protected ServiceConfig _config;

		public TbCurrencyBL(ServiceConfig environment)
		{
			_config = environment;
		}

		public async Task<TbCurrency> Insert(TbCurrency objInput)
		{
			TbCurrency lrespuesta = null;

			var ldata = await Task.FromResult(new TbCurrencyDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StDescription.Equals(objInput.StDescription));

				if (lmoneda != null && !string.IsNullOrEmpty(lmoneda.StCodCurrency))
					throw new Exception("Moneda duplicada");
			}

			objInput.StCodCurrency = DateTime.Now.ToString("yyyyMMddHHmmssff");

			lrespuesta = await Task.FromResult(new TbCurrencyDO(_config).Insert(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<TbCurrency> Update(TbCurrency objInput)
		{
			TbCurrency lrespuesta = null;

			var ldata = await Task.FromResult(new TbCurrencyDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StCodCurrency.Equals(objInput.StCodCurrency));

				if (lmoneda == null)
					throw new Exception("Moneda no existe");
			}

			lrespuesta = await Task.FromResult(new TbCurrencyDO(_config).Update(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<bool> Delete(TbCurrency objInput)
		{
			bool lrespuesta;

			var ldata = await Task.FromResult(new TbCurrencyDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StCodCurrency.Equals(objInput.StCodCurrency));

				if (lmoneda == null)
					throw new Exception("Moneda no existe");
			}

			lrespuesta = await Task.FromResult(new TbCurrencyDO(_config).Delete(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<TbCurrency[]> Select()
		{
			return await Task.FromResult(new TbCurrencyDO(_config).Select()).ConfigureAwait(false);
		}

		public async Task<TbCurrency> SelectBy(TbCurrency objInput)
		{
			return await Task.FromResult(new TbCurrencyDO(_config).SelectBy(objInput)).ConfigureAwait(false);
		}

		public async Task<TbCurrency> IsValidToken(TbCurrency objInput)
		{
			TbCurrency lrespuesta = null;

			var ldata = await Task.FromResult(new TbCurrencyDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StCodCurrency.Equals(objInput.StCodCurrency));

				if (lmoneda != null)
					lrespuesta = lmoneda;
			}

			return lrespuesta;
		}
	}
}