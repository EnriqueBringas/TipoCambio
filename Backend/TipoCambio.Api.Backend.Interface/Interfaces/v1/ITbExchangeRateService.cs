using System.Threading.Tasks;
using TipoCambio.Api.Backend.Entity.Entities.v1;
using TipoCambio.Api.Backend.Entity.Models;

namespace TipoCambio.Api.Backend.Interface.Interfaces.v1
{
	public interface ITbExchangeRateService
	{
		Task<TbExchangeRate> Insert(TbExchangeRate objInput);

		Task<TbExchangeRate> Update(TbExchangeRate objInput);

		Task<bool> Delete(TbExchangeRate objInput);

		Task<TbExchangeRate[]> Select();

		Task<TbExchangeRate> SelectBy(TbExchangeRate objInput);

		Task<ChangeMoneyRS> ChangeMoney(ChangeMoneyRQ objInput);
	}
}