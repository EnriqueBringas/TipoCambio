using System.Text.Json.Serialization;

namespace TipoCambio.Api.Core.DTO
{
    public class TokenRS
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }
}