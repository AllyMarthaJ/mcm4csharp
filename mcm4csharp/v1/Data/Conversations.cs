using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Conversations
{
	public record struct Conversation([property:JsonPropertyName("conversation_id")] ulong ConversationId,
					  [property:JsonPropertyName("title")] string Title,
					  [property:JsonPropertyName("creation_date")] ulong CreationDate,
					  [property:JsonPropertyName("creator_id")] ulong CreatorId,
					  [property:JsonPropertyName("last_message_date")] ulong LastMessageDate,
					  [property:JsonPropertyName("last_read_date")] ulong LastReadDate,
					  [property:JsonPropertyName("open")] bool Open,
					  [property:JsonPropertyName("reply_count")] ulong ReplyCount,
					  [property:JsonPropertyName("recipient_ids")] ulong[] RecipientIds);

	public record struct Reply([property:JsonPropertyName("message_id")] ulong MessageId,
				   [property:JsonPropertyName("message_date")] ulong MessageDate,
				   [property:JsonPropertyName("author_id")] ulong AuthorId,
				   [property:JsonPropertyName("message")] string Message);
}

