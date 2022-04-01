using System.Threading.Tasks;
using TipoCambio.Api.Backend.Entity.Entities.v1;

namespace TipoCambio.Api.Backend.Interface.Interfaces.v1
{
	public interface ITbCurrencyService
	{
		Task<TbCurrency> Insert(TbCurrency objInput);

		Task<TbCurrency> Update(TbCurrency objInput);

		Task<bool> Delete(TbCurrency objInput);

		Task<TbCurrency[]> Select();

		Task<TbCurrency> SelectBy(TbCurrency objInput);
	}
}