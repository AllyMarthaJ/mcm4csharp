using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Review {
		[JsonPropertyName ("review_id")]
		public uint ReviewId { get; set; }

		[JsonPropertyName ("reviewer_id")]
		public uint ReviewerId { get; set; }

		[JsonPropertyName ("review_date")]
		public uint ReviewDate { get; set; }

		[JsonPropertyName ("rating")]
		public uint Rating { get; set; }

		[JsonPropertyName ("message")]
		public string Message { get; set; }

		[JsonPropertyName ("response")]
		public string Response { get; set; }
	}
}

