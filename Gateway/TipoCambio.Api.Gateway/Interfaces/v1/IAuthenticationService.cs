using System.Threading.Tasks;
using TipoCambio.Api.Core.DTO;
using TipoCambio.Api.Core.Utility.Transfer.Request;

namespace TipoCambio.Api.Gateway.Interfaces.v1
{
    public interface IAuthenticationService
    {
        Task<TokenRS> Generate(RequestWith<TokenRQ> request);
    }
}