using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Conversations {
	public struct Conversation {
		[JsonPropertyName ("conversation_id")]
		public ulong ConversationId { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("creation_date")]
		public ulong CreationDate { get; set; }

		[JsonPropertyName ("creator_id")]
		public ulong CreatorId { get; set; }

		[JsonPropertyName ("last_message_date")]
		public ulong LastMessageDate { get; set; }

		[JsonPropertyName ("last_read_date")]
		public ulong LastReadDate { get; set; }

		[JsonPropertyName ("open")]
		public bool Open { get; set; }

		[JsonPropertyName ("reply_count")]
		public ulong ReplyCount { get; set; }

		[JsonPropertyName ("recipient_ids")]
		public ulong [] RecipientIds { get; set; }
	}
}

