using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct BasicResource {
		[JsonPropertyName ("resource_id")]
		public uint ResourceId { get; set; }

		[JsonPropertyName ("author_id")]
		public uint AuthorId { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("tag_line")]
		public string TagLine { get; set; }

		[JsonPropertyName ("price")]
		public float Price { get; set; }

		[JsonPropertyName ("currency")]
		public string Currency { get; set; }
	}
}

