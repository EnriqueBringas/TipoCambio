namespace TipoCambio.Api.Backend.Entity.Models
{
    public class ChangeMoneyRS
    {
        public string StCodCurrencyOrigin { get; set; }
        public string StCurrencyOrigin { get; set; }
        public string StCodCurrencyDestination { get; set; }
        public string StCurrencyDestination { get; set; }
        public decimal DcExchangeRate { get; set; }
        public decimal DcAmountBase { get; set; }
        public decimal DcAmountChange { get; set; }
    }
}