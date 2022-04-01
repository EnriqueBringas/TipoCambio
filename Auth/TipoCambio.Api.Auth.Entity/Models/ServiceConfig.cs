namespace TipoCambio.Api.Auth.Entity.Entities.Models
{
    public class ServiceConfig
    {
        public string Server { set; get; }
        public string User { set; get; }
        public string Password { set; get; }
        public string Database { set; get; }
        public string SecretKey { get; set; }
        public int MinutesToExpires { get; set; }
    }
}