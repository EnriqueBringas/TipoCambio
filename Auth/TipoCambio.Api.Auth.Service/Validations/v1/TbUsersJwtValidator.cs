using FluentValidation;
using TipoCambio.Api.Auth.Entity.Entities.v1;

namespace TipoCambio.Api.Auth.Service.Validations.v1
{
    internal class TbUsersJwtValidator : AbstractValidator<TbUsersJwt>
    {
        public TbUsersJwtValidator()
        {
            //switch (type)
            //{
            //    case "INS":
            //    case "UPD":
            //    case "DEL":
            //    case "SEL":
            //        break;

            //    case "SELBY":
            //        RuleFor(request => request.StUserName)
            //            .NotNull()
            //            .NotEmpty();

            //        RuleFor(request => request.StPassword)
            //            .NotNull()
            //            .NotEmpty();
            //        break;
            //}
        }
    }
}