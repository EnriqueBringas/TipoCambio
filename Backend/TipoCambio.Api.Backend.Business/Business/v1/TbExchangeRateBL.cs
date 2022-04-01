using System;
using System.Linq;
using System.Threading.Tasks;
using TipoCambio.Api.Backend.Data.Data.v1;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Backend.Entity.Entities.v1;
using TipoCambio.Api.Backend.Entity.Models;

namespace TipoCambio.Api.Backend.Business.Business.v1
{
	public class TbExchangeRateBL
	{
		protected ServiceConfig _config;

		public TbExchangeRateBL(ServiceConfig environment)
		{
			_config = environment;
		}

		public async Task<TbExchangeRate> Insert(TbExchangeRate objInput)
		{
			TbExchangeRate lrespuesta = null;

			var ldata = await Task.FromResult(new TbExchangeRateDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StDate.Equals(objInput.StDate) && c.StCodCurrencyOrigin.Equals(objInput.StCodCurrencyOrigin) && c.StCodCurrencyDestination.Equals(objInput.StCodCurrencyDestination));

				if (lmoneda != null && !string.IsNullOrEmpty(lmoneda.StCodExchangeRate))
					throw new Exception("Tipo de cambio duplicado");
			}

			objInput.StCodExchangeRate = DateTime.Now.ToString("yyyyMMddHHmmssff");

			lrespuesta = await Task.FromResult(new TbExchangeRateDO(_config).Insert(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<TbExchangeRate> Update(TbExchangeRate objInput)
		{
			TbExchangeRate lrespuesta = null;

			var ldata = await Task.FromResult(new TbExchangeRateDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StCodExchangeRate.Equals(objInput.StCodExchangeRate));

				if (lmoneda == null)
					throw new Exception("Tipo de cambio no existe");
			}

			lrespuesta = await Task.FromResult(new TbExchangeRateDO(_config).Update(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<bool> Delete(TbExchangeRate objInput)
		{
			bool lrespuesta;

			var ldata = await Task.FromResult(new TbExchangeRateDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StCodExchangeRate.Equals(objInput.StCodExchangeRate));

				if (lmoneda == null)
					throw new Exception("Tipo de cambio no existe");
			}

			lrespuesta = await Task.FromResult(new TbExchangeRateDO(_config).Delete(objInput)).ConfigureAwait(false);

			return lrespuesta;
		}

		public async Task<TbExchangeRate[]> Select()
		{
			return await Task.FromResult(new TbExchangeRateDO(_config).Select()).ConfigureAwait(false);
		}

		public async Task<TbExchangeRate> SelectBy(TbExchangeRate objInput)
		{
			return await Task.FromResult(new TbExchangeRateDO(_config).SelectBy(objInput)).ConfigureAwait(false);
		}

		public async Task<TbExchangeRate> IsValidToken(TbExchangeRate objInput)
		{
			TbExchangeRate lrespuesta = null;

			var ldata = await Task.FromResult(new TbExchangeRateDO(_config).Select()).ConfigureAwait(false);

			if (ldata != null && ldata.Any())
			{
				var lmoneda = ldata.ToList().Find(c => c.StCodExchangeRate.Equals(objInput.StCodExchangeRate));

				if (lmoneda != null)
					lrespuesta = lmoneda;
			}

			return lrespuesta;
		}

		public async Task<ChangeMoneyRS> ChangeMoney(ChangeMoneyRQ objInput)
		{
			ChangeMoneyRS lrespuesta = null;

			objInput.StDate = DateTime.Now.ToString(_config.FormatDate);

			var lcambio = await Task.FromResult(new TbExchangeRateDO(_config).SelectByDate(objInput)).ConfigureAwait(false);

			if (lcambio != null)
            {
				if (objInput.StCodCurrencyOrigin.Equals(lcambio.StCodCurrencyOrigin))
				{
					lrespuesta = new ChangeMoneyRS
					{
						StCodCurrencyOrigin = lcambio.StCodCurrencyOrigin,
						StCurrencyOrigin = lcambio.StCurrencyOrigin,
						StCodCurrencyDestination = lcambio.StCodCurrencyDestination,
						StCurrencyDestination = lcambio.StCurrencyDestination,
						DcExchangeRate = decimal.Round(lcambio.StCurrencyOrigin.Equals(_config.CurrencyLocal) ? lcambio.DcAmountSale : lcambio.DcAmountBuy, _config.Decimals),
						DcAmountBase = decimal.Round(objInput.DcAmount, _config.Decimals),
						DcAmountChange = decimal.Round(lcambio.StCurrencyOrigin.Equals(_config.CurrencyLocal) ? objInput.DcAmount / lcambio.DcAmountSale : objInput.DcAmount * lcambio.DcAmountBuy, _config.Decimals)
					};
				}
				else
				{
					lrespuesta = new ChangeMoneyRS
					{
						StCodCurrencyOrigin = lcambio.StCodCurrencyDestination,
						StCurrencyOrigin = lcambio.StCurrencyDestination,
						StCodCurrencyDestination = lcambio.StCodCurrencyOrigin,
						StCurrencyDestination = lcambio.StCurrencyOrigin,
						DcExchangeRate = decimal.Round(lcambio.StCurrencyDestination.Equals(_config.CurrencyLocal) ? lcambio.DcAmountSale : lcambio.DcAmountBuy, _config.Decimals),
						DcAmountBase = decimal.Round(objInput.DcAmount, _config.Decimals),
						DcAmountChange = decimal.Round(lcambio.StCurrencyDestination.Equals(_config.CurrencyLocal) ? objInput.DcAmount / lcambio.DcAmountSale : objInput.DcAmount * lcambio.DcAmountBuy, _config.Decimals)
					};
				}
			}

			return lrespuesta;
		}
	}
}