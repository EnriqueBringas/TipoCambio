using System.Threading.Tasks;
using TipoCambio.Api.Backend.Business.Business.v1;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Backend.Entity.Entities.v1;
using TipoCambio.Api.Backend.Entity.Models;
using TipoCambio.Api.Backend.Interface.Interfaces.v1;
using TipoCambio.Api.Backend.Service.Validations;
using TipoCambio.Api.Backend.Service.Validations.v1;

namespace TipoCambio.Api.Backend.Service.Services.v1
{
    public class TbExchangeRateService : BaseService, ITbExchangeRateService
    {
        public TbExchangeRateService(ServiceConfig environment)
        {
            _config = environment;
        }

        public async Task<TbExchangeRate> Insert(TbExchangeRate objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbExchangeRateValidator, TbExchangeRate>(objInput);

            return await new TbExchangeRateBL(_config).Insert(objInput);
        }

        public async Task<TbExchangeRate> Update(TbExchangeRate objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbExchangeRateValidator, TbExchangeRate>(objInput);

            return await new TbExchangeRateBL(_config).Update(objInput);
        }

        public async Task<bool> Delete(TbExchangeRate objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbExchangeRateValidator, TbExchangeRate>(objInput);

            return await new TbExchangeRateBL(_config).Delete(objInput);
        }

        public async Task<TbExchangeRate[]> Select()
        {
            return await new TbExchangeRateBL(_config).Select();
        }

        public async Task<TbExchangeRate> SelectBy(TbExchangeRate objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbExchangeRateValidator, TbExchangeRate>(objInput);

            return await new TbExchangeRateBL(_config).SelectBy(objInput);
        }

        public async Task<ChangeMoneyRS> ChangeMoney(ChangeMoneyRQ objInput)
        {
            ValidRequest(objInput);

            FluentHelper.ExecuteValidator<ChangeMoneyRQValidator, ChangeMoneyRQ>(objInput);

            return await new TbExchangeRateBL(_config).ChangeMoney(objInput);
        }
    }
}