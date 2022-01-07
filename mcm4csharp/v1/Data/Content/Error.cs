using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct Error {
		[JsonPropertyName ("code")]
		public string Code { get; set; }

		[JsonPropertyName ("message")]
		public string Message { get; set; }
	}
}

