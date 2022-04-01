using System.Text.Json.Serialization;

namespace TipoCambio.Api.Core.DTO
{
    public class ExchangeRS
	{
		[JsonPropertyName("result")]
		public ResultExchange Result { get; set; }

		[JsonPropertyName("status")]
		public Status Status { get; set; }
	}

	public class ExchangeDeleteRS
	{
		[JsonPropertyName("result")]
		public bool Result { get; set; }

		[JsonPropertyName("status")]
		public Status Status { get; set; }
	}

	public class ExchangeListRS
	{
		[JsonPropertyName("result")]
		public ResultExchange[] Result { get; set; }

		[JsonPropertyName("status")]
		public Status Status { get; set; }
	}

	public class ResultExchange
	{
		[JsonPropertyName("stCodExchangeRate")]
		public string StCodExchangeRate { get; set; }

		[JsonPropertyName("stDate")]
		public string StDate { get; set; }

		[JsonPropertyName("stCodCurrencyOrigin")]
		public string StCodCurrencyOrigin { get; set; }

		[JsonPropertyName("stCurrencyOrigin")]
		public string StCurrencyOrigin { get; set; }

		[JsonPropertyName("stCodCurrencyDestination")]
		public string StCodCurrencyDestination { get; set; }

		[JsonPropertyName("stCurrencyDestination")]
		public string StCurrencyDestination { get; set; }

		[JsonPropertyName("dcAmountBuy")]
		public decimal DcAmountBuy { get; set; }

		[JsonPropertyName("dcAmountSale")]
		public decimal DcAmountSale { get; set; }

		[JsonPropertyName("blIsDeleted")]
		public bool BlIsDeleted { get; set; }
	}
}