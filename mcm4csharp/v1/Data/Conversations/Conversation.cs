using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Conversations {
	public struct Conversation {
		[JsonPropertyName ("conversation_id")]
		public uint ConversationId { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("creation_date")]
		public uint CreationDate { get; set; }

		[JsonPropertyName ("creator_id")]
		public uint CreatorId { get; set; }

		[JsonPropertyName ("last_message_date")]
		public uint LastMessageDate { get; set; }

		[JsonPropertyName ("last_read_date")]
		public uint LastReadDate { get; set; }

		[JsonPropertyName ("open")]
		public bool Open { get; set; }

		[JsonPropertyName ("reply_count")]
		public uint ReplyCount { get; set; }

		[JsonPropertyName ("recipient_ids")]
		public uint [] RecipientIds { get; set; }
	}
}

