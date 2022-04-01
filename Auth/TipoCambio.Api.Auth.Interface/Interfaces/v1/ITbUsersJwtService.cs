using System.Threading.Tasks;
using TipoCambio.Api.Auth.Entity.Entities.v1;

namespace TipoCambio.Api.Auth.Interface.Interfaces.v1
{
    public interface ITbUsersJwtService
	{
		Task<TbUsersJwt> Insert(TbUsersJwt objInput);

		Task<TbUsersJwt> Update(TbUsersJwt objInput);

		Task<bool> Delete(TbUsersJwt objInput);

		Task<TbUsersJwt[]> Select();

		Task<TbUsersJwt> SelectBy(TbUsersJwt objInput);

		Task<TbUsersJwt> IsValidToken(TbUsersJwt objInput);
	}
}