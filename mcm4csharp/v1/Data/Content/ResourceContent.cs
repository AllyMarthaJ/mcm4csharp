using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct ResourceContent {
		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("tag_line")]
		public string Tagline { get; set; }

		[JsonPropertyName ("description")]
		public string Description { get; set; }
	}
}

