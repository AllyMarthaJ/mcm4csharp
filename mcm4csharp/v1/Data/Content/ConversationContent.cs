using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct ConversationContent {
		[JsonPropertyName("recipient_ids")]
		public uint[] RecipientIds { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}

