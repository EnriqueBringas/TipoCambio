using System.Threading.Tasks;
using TipoCambio.Api.Core.DTO;
using TipoCambio.Api.Core.Utility.Transfer.Request;

namespace TipoCambio.Api.Gateway.Interfaces.v1
{
    public interface IBackendService
    {
        Task<CurrencyRS> Currency(Request request);

        Task<ChangeMoneyRS> ChangeMoney(RequestWith<ChangeMoneyRQ> request);

        Task<ExchangeRS> InsertExchange(RequestWith<ExchangeRQ> request);
        Task<ExchangeRS> UpdateExchange(RequestWith<ExchangeRQ> request);
        Task<ExchangeDeleteRS> DeleteExchange(RequestWith<ExchangeRQ> request);
        Task<ExchangeListRS> SelectExchange(Request request);
    }
}