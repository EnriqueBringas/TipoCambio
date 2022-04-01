using FluentValidation;
using TipoCambio.Api.Backend.Entity.Entities.v1;

namespace TipoCambio.Api.Backend.Service.Validations.v1
{
    internal class TbExchangeRateValidator : AbstractValidator<TbExchangeRate>
    {
        public TbExchangeRateValidator(string type)
        {
            switch (type)
            {
                case "INS":
                case "UPD":
                case "DEL":
                case "SEL":
                    break;

                case "SELBY":
                    RuleFor(request => request.StCodExchangeRate)
                        .NotNull()
                        .NotEmpty();
                    break;
            }
        }
    }
}