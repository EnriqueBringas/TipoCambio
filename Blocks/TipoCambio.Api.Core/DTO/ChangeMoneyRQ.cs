namespace TipoCambio.Api.Core.DTO
{
    public class ChangeMoneyRQ
    {
        public string StCodCurrencyOrigin { get; set; }
        public string StCodCurrencyDestination { get; set; }
        public decimal DcAmount { get; set; }
    }
}