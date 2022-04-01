using FluentValidation;
using TipoCambio.Api.Backend.Entity.Entities.v1;

namespace TipoCambio.Api.Backend.Service.Validations.v1
{
    internal class TbCurrencyValidator : AbstractValidator<TbCurrency>
    {
        public TbCurrencyValidator(string type)
        {
            switch (type)
            {
                case "INS":
                case "UPD":
                case "DEL":
                case "SEL":
                    break;

                case "SELBY":
                    RuleFor(request => request.StCodCurrency)
                        .NotNull()
                        .NotEmpty();
                    break;
            }
        }
    }
}