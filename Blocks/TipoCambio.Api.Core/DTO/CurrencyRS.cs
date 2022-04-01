using System.Text.Json.Serialization;

namespace TipoCambio.Api.Core.DTO
{
    public class CurrencyRS
    {
        [JsonPropertyName("result")]
        public ResultCurrency[] Result { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }

    public class ResultCurrency
    {
        [JsonPropertyName("stCodCurrency")]
        public string StCodCurrency { get; set; }

        [JsonPropertyName("stDescription")]
        public string StDescription { get; set; }

        [JsonPropertyName("blIsDeleted")]
        public bool BlIsDeleted { get; set; }
    }
}