using FluentValidation;
using TipoCambio.Api.Backend.Entity.Models;

namespace TipoCambio.Api.Backend.Service.Validations.v1
{
    internal class ChangeMoneyRQValidator : AbstractValidator<ChangeMoneyRQ>
    {
        public ChangeMoneyRQValidator()
        {
            RuleFor(request => request.StCodCurrencyOrigin)
                .NotNull()
                .NotEmpty();

            RuleFor(request => request.StCodCurrencyDestination)
                .NotNull()
                .NotEmpty();

            RuleFor(request => request.DcAmount)
                .GreaterThan(0);
        }
    }
}