namespace TipoCambio.Api.Backend.Entity.Models
{
    public class ChangeMoneyRQ
    {
        public string StDate { get; set; }
        public string StCodCurrencyOrigin { get; set; }
        public string StCodCurrencyDestination { get; set; }
        public decimal DcAmount { get; set; }
    }
}