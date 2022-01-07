using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct ConversationContent {
		[JsonPropertyName("recipients_ids")]
		public uint[] RecipientsId { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}

