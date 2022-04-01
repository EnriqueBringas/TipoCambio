using System.Text.Json.Serialization;

namespace TipoCambio.Api.Core.DTO
{
    public class ChangeMoneyRS
    {
        [JsonPropertyName("result")]
        public ResultChangeMoney Result { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }

    public class ResultChangeMoney
    {
        [JsonPropertyName("stCodCurrencyOrigin")]
        public string StCodCurrencyOrigin { get; set; }

        [JsonPropertyName("stCurrencyOrigin")]
        public string StCurrencyOrigin { get; set; }

        [JsonPropertyName("stCodCurrencyDestination")]
        public string StCodCurrencyDestination { get; set; }

        [JsonPropertyName("stCurrencyDestination")]
        public string StCurrencyDestination { get; set; }

        [JsonPropertyName("dcExchangeRate")]
        public decimal DcExchangeRate { get; set; }

        [JsonPropertyName("dcAmountBase")]
        public decimal DcAmountBase { get; set; }

        [JsonPropertyName("dcAmountChange")]
        public decimal DcAmountChange { get; set; }
    }
}