namespace TipoCambio.Api.Core.DTO
{
    public class ExchangeRQ
	{
		public string StCodExchangeRate { get; set; }
		public string StDate { get; set; }
		public string StCodCurrencyOrigin { get; set; }
		public string StCodCurrencyDestination { get; set; }
		public decimal DcAmountBuy { get; set; }
		public decimal DcAmountSale { get; set; }
	}
}