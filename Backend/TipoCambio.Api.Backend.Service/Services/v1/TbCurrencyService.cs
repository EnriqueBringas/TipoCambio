using System.Threading.Tasks;
using TipoCambio.Api.Backend.Business.Business.v1;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Backend.Entity.Entities.v1;
using TipoCambio.Api.Backend.Interface.Interfaces.v1;
using TipoCambio.Api.Backend.Service.Validations;
using TipoCambio.Api.Backend.Service.Validations.v1;

namespace TipoCambio.Api.Backend.Service.Services.v1
{
    public class TbCurrencyService : BaseService, ITbCurrencyService
    {
        public TbCurrencyService(ServiceConfig environment)
        {
            _config = environment;
        }

        public async Task<TbCurrency> Insert(TbCurrency objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbCurrencyValidator, TbCurrency>(objInput);

            return await new TbCurrencyBL(_config).Insert(objInput);
        }

        public async Task<TbCurrency> Update(TbCurrency objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbCurrencyValidator, TbCurrency>(objInput);

            return await new TbCurrencyBL(_config).Update(objInput);
        }

        public async Task<bool> Delete(TbCurrency objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbCurrencyValidator, TbCurrency>(objInput);

            return await new TbCurrencyBL(_config).Delete(objInput);
        }

        public async Task<TbCurrency[]> Select()
        {
            return await new TbCurrencyBL(_config).Select();
        }

        public async Task<TbCurrency> SelectBy(TbCurrency objInput)
        {
            ValidRequest(objInput);

            //FluentHelper.ExecuteValidator<TbCurrencyValidator, TbCurrency>(objInput);

            return await new TbCurrencyBL(_config).SelectBy(objInput);
        }
    }
}