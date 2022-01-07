using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct MessageContent {
		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}

