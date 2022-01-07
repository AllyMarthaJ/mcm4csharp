using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct SelfUpdateContent {
		[JsonPropertyName("custom_title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string CustomTitle { get; set; }

		[JsonPropertyName("about_me")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string AboutMe { get; set; }

		[JsonPropertyName("signature")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string Signature { get; set; }
	}
}

