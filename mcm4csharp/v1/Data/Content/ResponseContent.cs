using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct ResponseContent {
		[JsonPropertyName("response")]
		public string Response { get; set; }
	}
}

