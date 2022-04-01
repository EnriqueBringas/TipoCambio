namespace TipoCambio.Api.Backend.Entity.Entities.v1
{
    public class TbExchangeRate
	{
		public string StCodExchangeRate { get; set; }
		public string StDate { get; set; }
		public string StCodCurrencyOrigin { get; set; }
        public string StCurrencyOrigin { get; set; }
		public string StCodCurrencyDestination { get; set; }
		public string StCurrencyDestination { get; set; }
		public decimal DcAmountBuy { get; set; }
		public decimal DcAmountSale { get; set; }
		public bool BlIsDeleted { get; set; }
    }
}