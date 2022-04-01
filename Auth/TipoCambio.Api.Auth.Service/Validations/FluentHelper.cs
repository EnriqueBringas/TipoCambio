using FluentValidation;
using System;

namespace TipoCambio.Api.Auth.Service.Validations
{
    public static class FluentHelper
    {
        internal static void ExecuteValidator<TValidator, TParameters>(TParameters parameters) where TParameters : class where TValidator : AbstractValidator<TParameters>
        {
            Activator.CreateInstance<TValidator>().ValidateAndThrow(parameters);
        }
    }
}