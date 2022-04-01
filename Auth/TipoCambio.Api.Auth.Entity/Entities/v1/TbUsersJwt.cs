namespace TipoCambio.Api.Auth.Entity.Entities.v1
{
    public class TbUsersJwt
	{
		public string StCodUserJwt { get; set; }
		public string StUserName { get; set; }
		public string StPassword { get; set; }
		public string StSalt { get; set; }
		public bool BlIsActive { get; set; }
		public bool BlIsDeleted { get; set; }
	}
}