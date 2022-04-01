using System.Text.Json.Serialization;

namespace TipoCambio.Api.Core.DTO
{
    public class Status
	{
		[JsonPropertyName("ok")]
		public bool Ok { set; get; }

		[JsonPropertyName("messages")]
		public Message[] Messages { set; get; }

		[JsonPropertyName("tracking")]
		public string Tracking { set; get; }
	}

	public class Message
	{
		[JsonPropertyName("isError")]
		public bool IsError { set; get; }

		[JsonPropertyName("value")]
		public string Value { set; get; }
	}
}
